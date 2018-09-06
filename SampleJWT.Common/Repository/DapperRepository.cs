using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using DapperExtensions;

namespace SampleJWT.Repository
{
    public class DapperRepository<T> : IRepository<T>
        where T: class
    {
        public DapperRepository()
        {

        }
        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            using (IDbConnection db = new SqlConnection(SampleJWT.Properties.Settings.Default.DatabaseConnection))
            {
                return db.GetAll<T>().ToList();
            }
        }

        public IEnumerable<T> Get(IPredicate predicate)
        {
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(AnnotationCustomMapper<>);
            using (IDbConnection db = new SqlConnection(SampleJWT.Properties.Settings.Default.DatabaseConnection))
            {
                return db.GetList<T>(predicate).ToList();
            }
        }
    }
}
