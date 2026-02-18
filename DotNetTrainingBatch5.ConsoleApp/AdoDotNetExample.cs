using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class AdoDotNetExample
    {
        protected readonly string _connection = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=p@ssw0rd";

        public void Read()
        {
            //string connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=p@ssw0rd";
            Console.WriteLine(value: "Connection String:" + _connection);
            SqlConnection connection = new SqlConnection(_connection);

            Console.WriteLine("Connection Opening...");
            connection.Open();
            Console.WriteLine("Connection Opened");

            //Command = Query
            string query = @"SELECT * FROM TBL_BLOG";
            SqlCommand cmd = new SqlCommand(query, connection);

            #region "DataAdapter"
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            #endregion

            #region "DataReader"
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"] + "|" + reader["BlogTitle"] + "|" + reader["BlogAuthor"] + "|" + reader["BlogContent"]);
            }
            #endregion

            //dt = adapter.execute();
            //for vs foreach (foreach better)

            //DataSet
            //DataTable
            //DataRow
            //DataColumn

            Console.WriteLine("Connection Closing");
            connection.Close();
            Console.WriteLine("Connection Closed");

            //C# mhr data table htae pp so yin con close htr lae use loh ya tl
            #region "DataAdapter"
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"] + "|" + dr["BlogTitle"] + "|" + dr["BlogAuthor"] + "|" + dr["BlogContent"]);
            //}
            #endregion
        }

        public void Create()
        {
            Console.Write("Enter Blog Tite");
            string Title = Console.ReadLine();

            Console.Write("Enter Blog Author");
            string Author = Console.ReadLine();

            Console.Write("Enter Blog Content");
            string Content = Console.ReadLine();


            //string connectionString1 = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=p@ssw0rd";
            SqlConnection connection = new SqlConnection(_connection);

            connection.Open();
            //string insquery = $@"INSERT INTO [dbo].[Tbl_Blog]
            //           ([BlogTitle]
            //           ,[BlogAuthor]
            //           ,[BlogContent]
            //           ,[DeleteFlag])
            //     VALUES
            //           ('{Title}' 
            //           ,'{Author}'
            //           ,'{Content}'
            //           ,0)";

            string insquery = $@"insert into [dbo].[tbl_blog]
               ([blogtitle]
               ,[blogauthor]
               ,[blogcontent]
               ,[deleteflag])
            values
               (@blogtitle 
               ,@blogauthor
               ,@blogcontent
               ,0)";

            //string so yin '_' need (imp)

            SqlCommand insCmd = new SqlCommand(insquery, connection);
            //For sql injection protection
            insCmd.Parameters.AddWithValue("@blogtitle", Title);
            insCmd.Parameters.AddWithValue("@blogauthor", Author);
            insCmd.Parameters.AddWithValue("@blogcontent", Content);
            //SqlDataAdapter adapter = new SqlDataAdapter(insCmd);
            //DataTable db = new DataTable();
            //adapter.Fill(db);

            //foreach (DataRow drow in db.Rows)
            //{
            //    Console.WriteLine(drow["BlogId"] + "|" + drow["BlogTitle"] + "|" + drow["BlogAuthor"] + "|" + drow["BlogContent"]);
            //}

            int result = insCmd.ExecuteNonQuery();

            //if(result == 1)
            //{
            //    Console.WriteLine("Success Insert");
            //}
            //else
            //{
            //    Console.WriteLine("Success Fail");
            //}

            Console.WriteLine(result == 1 ? "Success Insert" : "Fail");
            connection.Close();
        }

        public void Extract()
        {
            Console.WriteLine("Enter Id to Search");
            string id = Console.ReadLine();

            SqlConnection excon = new SqlConnection(_connection);
            excon.Open();
            string exquery = "Select * from [dbo].[Tbl_Blog] where BlogId=@blog";
            SqlCommand ecmd = new SqlCommand(exquery, excon);
            ecmd.Parameters.AddWithValue("@blog", id);
            SqlDataReader reader = ecmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    Console.WriteLine(reader["BlogId"] + "|" + reader["BlogTitle"] + "|" + reader["BlogAuthor"] + "|" + reader["BlogContent"]);
                }
            }

            excon.Close();
        }

        public void Update()
        {
            SqlConnection updcon = new SqlConnection(_connection);
            updcon.Open();
            Console.WriteLine("Enter Id");
            string id = Console.ReadLine();

            Console.WriteLine("Enter Blog Title");
            string title = Console.ReadLine();

            Console.WriteLine("Enter Blog Author");
            string author = Console.ReadLine();

            Console.WriteLine("Enter Blog Description");
            string content = Console.ReadLine();

            string upd = @"UPDATE [dbo].[Tbl_Blog]
                              SET  [BlogTitle] = @blogTitle
                                  ,[BlogAuthor] = @blogAuthor
                                  ,[BlogContent] = @blogContent
                                  ,[DeleteFlag] = 0
                              WHERE [BlogId] = @blogId";

            SqlCommand updcmd = new SqlCommand(upd, updcon);

            updcmd.Parameters.AddWithValue("@blogId", id);
            updcmd.Parameters.AddWithValue("@blogTitle", title);
            updcmd.Parameters.AddWithValue("@blogAuthor", author);
            updcmd.Parameters.AddWithValue("@blogContent", content);

            int result = updcmd.ExecuteNonQuery();

            Console.WriteLine(result != 0 ? "Update Succeess" : "Fail Update");

            Extract();

            updcon.Close();
        }

        public void Delete()
        {
            Extract();
            Console.WriteLine("Enter Id to Delete");
            string id = Console.ReadLine();

            SqlConnection delcon = new SqlConnection(_connection);
            delcon.Open();
            string delstr = @"DELETE FROM [dbo].[Tbl_Blog]
                              WHERE [BlogId] = @blogId";
            SqlCommand delcmd = new SqlCommand(delstr, delcon);
            delcmd.Parameters.AddWithValue("@blogId", id);
            int result = delcmd.ExecuteNonQuery();

            Console.WriteLine(result!=0?"Delete Success":"Fail Delete");
            Read();
            delcon.Close();
        }
    }
}
