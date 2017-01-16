using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _context;
        private bool _disposed;

        public UnitOfWork(EFContext context)
        {
            this._context = context;
            this.Employees = new EmployeeRepository(context);
        }

        public IEmployeeRepository Employees { get; private set; }


        public int Complete()
        {
            return this._context.SaveChanges();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true; 
        }
    }
}
