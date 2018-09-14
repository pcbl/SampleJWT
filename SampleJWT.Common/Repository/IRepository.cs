using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleJWT.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Get(IPredicate predicate);

        IEnumerable<T> Get(IPredicate predicate, IList<ISort> sort, int page, int pageSize);

        T GetById(int id);

        long Count(IPredicate predicate);
    }
}
