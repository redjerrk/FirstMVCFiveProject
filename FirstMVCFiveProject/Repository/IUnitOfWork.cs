
using System;
using FirstMVCFiveProject.Models;
using FirstMVCFiveProject.Repositories;

namespace FirstMVCFiveProject.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Students { get; }
        int Complete();
    }
}