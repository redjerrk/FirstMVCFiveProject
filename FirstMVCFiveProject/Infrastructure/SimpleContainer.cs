
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace FirstMVCFiveProject.Infrastructure
{
    // Minimal DI container + MVC IDependencyResolver adapter.
    public class SimpleContainer : IDependencyResolver
    {
        private readonly ConcurrentDictionary<Type, Func<object>> _factories = new ConcurrentDictionary<Type, Func<object>>();

        public void Register<TService>(Func<object> factory)
        {
            _factories[typeof(TService)] = factory;
        }

        public void Register<TService, TImpl>() where TImpl : TService
        {
            _factories[typeof(TService)] = () => CreateInstance(typeof(TImpl));
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type serviceType)
        {
            if (_factories.TryGetValue(serviceType, out var factory))
            {
                return factory();
            }

            // If caller asks for concrete type, try to construct it.
            if (!serviceType.IsAbstract && !serviceType.IsInterface)
            {
                return CreateInstance(serviceType);
            }

            return null;
        }

        private object CreateInstance(Type implType)
        {
            var ctor = implType.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
            if (ctor == null) return Activator.CreateInstance(implType);

            var parameters = ctor.GetParameters()
                .Select(p => Resolve(p.ParameterType) ?? CreateDefaultValue(p.ParameterType))
                .ToArray();

            return Activator.CreateInstance(implType, parameters);
        }

        private object CreateDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        // IDependencyResolver implementation for MVC
        public object GetService(Type serviceType)
        {
            return Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var resolved = Resolve(serviceType);
            return resolved == null ? Enumerable.Empty<object>() : new[] { resolved };
        }
    }
}