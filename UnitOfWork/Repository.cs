using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            this.Context = context;
        }

        public T Add(T t)
        {
            return Context.Set<T>().Add(t);
        }

        public T Get(Guid guid)
        {
            return Context.Set<T>().Find(guid);
        }

        public T Remove(T t)
        {
            return Context.Set<T>().Remove(t);
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).ToList();
        }
    }
}
