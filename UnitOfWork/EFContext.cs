using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public class EFContext : DbContext
    {
        public EFContext()
            : base("EFContext")
        {
        }


        public virtual DbSet<Employee> Employees { get; set; }

    }
}
