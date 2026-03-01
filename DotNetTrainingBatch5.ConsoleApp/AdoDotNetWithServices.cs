using DotNetTrainingBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class AdoDotNetWithServices
    {
        private readonly string _connection = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True";
        private readonly AdoDotNetServices _service;

        public AdoDotNetWithServices()
        {
            _service = new AdoDotNetServices(_connection);
        }

        public void Read()
        {
            string query = "Select * from Tbl_Blog where DeleteFlag=0";
            DataTable dt = _service.Query(query);
            foreach (DataRow row in dt.Rows) 
            {
                Console.WriteLine(row["BlogId"] + "|" + row["BlogTitle"] + "|" + row["BlogAuthor"] + "|" + row["BlogContent"]);
            }    
        }

        public void ReadbyId()
        {
            Console.WriteLine("Enter an Id");
            string? id = Console.ReadLine();

            if (!string.IsNullOrEmpty(id))
            {
                string query = "Select * from Tbl_Blog where DeleteFlag=0 and BlogId=@id";
                DataTable dt = _service.Query(query, new Param("@id", id));

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row["BlogId"] + "|" + row["BlogTitle"] + "|" + row["BlogAuthor"] + "|" + row["BlogContent"]);
                }
            }
            else
            {
                Console.WriteLine("Id cannot be left blank!");
            }
        }

        public void Create()
        {
            Console.Write("Enter Blog Tite >> ");
            string? Title = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Enter Blog Author >> ");
            string? Author = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Enter Blog Content >> ");
            string? Content = Console.ReadLine();

            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Author) && !string.IsNullOrEmpty(Content)) 
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

                int result = _service.Execute(insquery,
                    new Param("@blogtitle",Title),
                    new Param("@blogauthor",Author),
                    new Param("@blogcontent",Content)
                    );

                Console.WriteLine(result == 1 ? "Success Insert" : "Fail");

                Read();
            }
            else
            {
                Console.WriteLine("Data Cannot be left blank!");
            }
            
        }

        public void Extract(int id = 4)
        {
            string query = "Select * from Tbl_Blog where DeleteFlag=0 and BlogId=@id";
            DataTable dt = _service.Query(query, new Param("@id",id));
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["BlogId"] + "|" + row["BlogTitle"] + "|" + row["BlogAuthor"] + "|" + row["BlogContent"]);
            }
        }

        public void Update()
        {
            Console.Write("Enter Id >> ");
            string? id = Console.ReadLine();

            Extract(Convert.ToInt32(id));

            if (id != null) 
            {
                Console.WriteLine("Enter Title >> ");
                string? title = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Enter Author >> ");
                string? author = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Enter Content >> ");
                string? content = Console.ReadLine();
                Console.WriteLine();

                string upd = @"UPDATE [dbo].[Tbl_Blog]
                              SET  [BlogTitle] = @blogTitle
                                  ,[BlogAuthor] = @blogAuthor
                                  ,[BlogContent] = @blogContent
                                  ,[DeleteFlag] = 0
                              WHERE [BlogId] = @blogId";

                int result = _service.Execute(upd, 
                    new Param("@blogId", id),
                    new Param("@blogTitle",title),
                    new Param("@blogAuthor",author),
                    new Param("@blogContent",content)
                    );
                Console.WriteLine(result == 1 ? "Update Success" : "Fail Update");
                Extract(Convert.ToInt32(id));
            }
        }

        public void Delete()
        {
            Read();

            Console.WriteLine("Choose Id to Delete");
            string? id = Console.ReadLine();

            if (id != null)
            {
                string query = "Delete from Tbl_Blog where BlogId=@id";

                int result = _service.Execute(query, new Param("@id", id));
                Console.WriteLine(result == 1 ? "Delete Success" : "Fail Delete");
                Read();
            }
            else
            {
                Console.WriteLine("Id cannot be left blank");
            }
        }
    }
}
 