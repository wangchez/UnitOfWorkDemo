using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace UnitOfWork
{
    public interface IRepository<T>
    {
        T Add(T t);

        T Get(Guid guid);

        T Remove(T t);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
