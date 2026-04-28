using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoClient.Services
{
    public class RefitExample
    {
        public async Task RunAsync()
        {
            var iblogapi = RestService.For<IBlogApi>("https://localhost:7027");
            var octocat = await iblogapi.GetBlogs();
            foreach (var blog in octocat) 
            {
                Console.WriteLine(blog.BlogTitle + "-" + blog.BlogAuthor + "-" + blog.BlogContent);
            }

            try
            {
                var item2 = await iblogapi.GetBlog(1);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound) 
                {
                    Console.WriteLine("No data Found");
                }
            }

            var item4 = await iblogapi.CreateBlog(new ResTblBlog
            {
                BlogTitle = "New tile",
                BlogAuthor = "New auth",
                BlogContent = "hello"
            });

        }
    }
}
