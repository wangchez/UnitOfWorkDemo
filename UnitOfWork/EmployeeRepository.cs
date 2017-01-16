using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EFContext context) : base(context)
        {

        }

        public IEnumerable<Employee> GetTopEmployees(int count)
        {
            return EFContext.Employees.OrderBy(e => e.Name).Take(count).ToList();
        }



        public EFContext EFContext
        {
            get { return Context as EFContext; }
        }
    }
}
