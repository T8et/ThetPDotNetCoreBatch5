using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.Shared
{
    public class DapperServices
    {
        private readonly string _connection;

        public DapperServices(string connection)
        {
            _connection = connection;
        }

        public List<T> Query<T>(string query)
        {
            using(IDbConnection db = new SqlConnection(_connection))
            {
                var list = db.Query<T>(query).ToList();
                return list;
            }
        }
    }
}
