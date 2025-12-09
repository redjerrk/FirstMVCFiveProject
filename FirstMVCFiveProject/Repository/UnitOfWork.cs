using System;
using FirstMVCFiveProject.Data;
using FirstMVCFiveProject.Models;
using FirstMVCFiveProject.Repositories;

namespace FirstMVCFiveProject.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Student> _students;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Student> Students => _students ?? (_students = new Repository<Student>(_context));

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}