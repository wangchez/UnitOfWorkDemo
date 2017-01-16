using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> GetTopEmployees(int count);
    }
}
