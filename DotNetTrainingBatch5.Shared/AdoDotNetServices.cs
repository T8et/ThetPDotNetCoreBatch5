using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.Shared
{
    public class AdoDotNetServices
    {
        private readonly string _connection;

        public AdoDotNetServices(string connection)
        {
            _connection = connection;
        }

        public DataTable Query(string query, params Param[] param)
        {
            SqlConnection con = new SqlConnection( _connection );
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            
            if(param is not null)
            {
                foreach (var item in param)
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
                }
            }

            SqlDataAdapter adpater = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpater.Fill(dt);
            con.Close();

            return dt;
        }

        public int Execute(string query, params Param[] param)
        {
            SqlConnection con = new SqlConnection(_connection);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);

            if (param is not null)
            {
                foreach (var item in param)
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
                }
            }

            int result = cmd.ExecuteNonQuery();
            con.Close();

            return result;
        }
    }

    public class Param
    {
        public string? Name { get; set; }

        public object? Value { get; set; }

        public Param(string? name, object? value)
        {
            Name = name;
            Value = value;
        }
    }
}
