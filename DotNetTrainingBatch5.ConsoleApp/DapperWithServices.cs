using DotNetTrainingBatch5.ConsoleApp.Models;
using DotNetTrainingBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class DapperWithServices
    {
        private readonly string _connection = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True";
        private readonly DapperServices _dapservice;

        public DapperWithServices()
        {
            _dapservice = new DapperServices(_connection);
        }

        public void Read() 
        {
            string query = "Select * from Tbl_Blog where DeleteFlag=0";

            var data = _dapservice.Query<BlogDataModel>(query).ToList();

            foreach (var item in data) 
            {
                Console.WriteLine(item.BlogId + "-" + item.BlogAuthor + "-" + item.BlogTitle + "-" + item.BlogContent);
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


            int result = _dapservice.Execute(insquery, new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });
            Console.WriteLine(result == 1 ? "Created Successfully" : "Failed Create");
            Read();
        }

        public void Update(string title, string author,string content)
        {
            Console.WriteLine("Enter Id to Update");
            string? id = Console.ReadLine();

            if (!string.IsNullOrEmpty(id))
            {
                string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @blogTitle
                              ,[BlogAuthor] = @blogAuthor
                              ,[BlogContent] = @blogContent
                              ,[DeleteFlag] = 0
                           WHERE [BlogId] = @blogId";

                var result = _dapservice.Execute(query, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                    BlogId = Convert.ToInt32(id)
                });

                Console.WriteLine(result == 1 ? "Update Success" : "Fail Update");

                if(result == 1)
                {
                    string equery = "Select * from Tbl_Blog where BlogId=@blogid and DeleteFlag=0";
                    var res = _dapservice.QueryFirstorDefault<BlogDataModel>(equery, new BlogDataModel
                    {
                        BlogId = Convert.ToInt32(id)
                    });
                    Console.WriteLine(res.BlogId+"-"+res.BlogTitle+"-"+res.BlogAuthor+"-"+res.BlogContent);
                }
                else
                {
                    Console.WriteLine("Fail");
                }
            }
            else
            {
                Console.WriteLine("Id cannot be null!");
            }
            
        }

        public void Delete()
        {
            Console.WriteLine("Enter ID to Delete");
            int id = Convert.ToInt32(Console.ReadLine());

            if(id > 0)
            {
                string equery = "Select * from Tbl_Blog where BlogId=@blogid and DeleteFlag=0";
                var res = _dapservice.QueryFirstorDefault<BlogDataModel>(equery, new BlogDataModel
                {
                    BlogId = id
                });
                Console.WriteLine(res.BlogId + "-" + res.BlogTitle + "-" + res.BlogAuthor + "-" + res.BlogContent);

                Console.WriteLine("Do you want to Delete 0-No 1-Yes");
                int prompt = Convert.ToInt32(Console.ReadLine());

                if (prompt == 1)
                {
                    string delquery = "Delete from Tbl_Blog where BlogId=@blogid and DeleteFlag=0";
                    int dres = _dapservice.Execute(delquery, new
                    {
                        blogid = id
                    });
                    Console.WriteLine(dres == 1 ? "Delete Success" : "Fail Delete");
                }
                else if (prompt == 0) 
                {
                    Console.WriteLine("Bye Bye...");
                }
            }
        }
    }
}
