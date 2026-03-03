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

        private readonly string _date = DateTime.Now.ToString();

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

        public T QueryFirstorDefault<T>(string query, object? param = null)
        {
            using(IDbConnection db = new SqlConnection(_connection))
            {
                var item = db.QueryFirstOrDefault<T>(query, param);
                return item;
            }
        }

        public int Execute(string query, object? param = null) 
        {
            Console.WriteLine("..............................");
            Console.WriteLine("Date  >> " + _date);
            Console.WriteLine("Param >> " + param.ToString());
            Console.WriteLine("Query >> " + query);
            Console.WriteLine("..............................");
            using IDbConnection db = new SqlConnection(_connection);
            var res = db.Execute(query, param);
            return res;
        }
    }
}
