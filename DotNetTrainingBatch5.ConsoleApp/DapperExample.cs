using Dapper;
using DotNetTrainingBatch5.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class DapperExample
    {
        protected readonly string _connection = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=p@ssw0rd";

        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_connection))
            {
                string query = "SELECT * FROM TBL_BLOG WHERE DeleteFlag=0";
                var list = db.Query(query).ToList();
                foreach (var item in list)
                {
                    Console.Write(item.BlogId + "-");
                    Console.Write(item.BlogTitle + "-");
                    Console.Write(item.BlogAuthor + "-");
                    Console.Write(item.BlogContent + "-");
                    Console.WriteLine("");
                }
            }
        }

        public void Create(string title, string author, string content)
        {
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

            using (IDbConnection con = new SqlConnection(_connection))
            {
                // Tested with param like obj
                //int result = con.Execute(insquery,
                //    new
                //    {
                //        blogtitle = title,
                //        blogauthor = author,
                //        blogcontent = content
                //    });
                // Tested with data model
                int result = con.Execute(insquery, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine(result == 1 ? "Created Successfully" : "Failed Create");
            }
        }

        public void Update(string title,string author,string content,int id)
        {
            string upd = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @blogTitle
                              ,[BlogAuthor] = @blogAuthor
                              ,[BlogContent] = @blogContent
                              ,[DeleteFlag] = 0
                           WHERE [BlogId] = @blogId";

            using (IDbConnection con = new SqlConnection(_connection)) 
            {
                //int result = con.Execute(upd, new
                //{
                //    blogTitle = title,
                //    blogAuthor = author,
                //    blogContent = content,
                //    blogId = id
                //});

                int result = con.Execute(upd, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                    BlogId = id
                });
                Console.WriteLine(result == 1 ? "Updated Successfully" : "Failed Updated");
            }
        }

        public void Delete(int id1)
        {
            string delquery = @"Delete from TBL_BLOG where BlogId=@BlogId";

            using (IDbConnection conn = new SqlConnection(_connection)) 
            {
                int result = conn.Execute(delquery, new BlogDataModel
                {
                    BlogId = id1
                });
                Console.WriteLine(result == 1 ? "Deleted Successfully":"Failed Delete");
            }
        }
    }
}

//DTO - Data Transfer Object
//28min
