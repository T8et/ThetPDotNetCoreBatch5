using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoClient.Services
{
    public class RefitStudy
    {
        public async Task RunAsnyc()
        {
            var refitApi = RestService.For<IBlogApi1>("https://localhost:7027");
            var octocat = await refitApi.GetBlogs();

            foreach (var blog in octocat) 
            {
                Console.WriteLine(blog.BlogTitle + "-" + blog.BlogAuthor + "-" + blog.BlogContent) ;
            }

            try
            {
                var newitem = await refitApi.GetBlogs(3);
                foreach (var blog in newitem)
                {
                    Console.WriteLine(blog.BlogTitle + "-" + blog.BlogAuthor + "-" + blog.BlogContent);
                }
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No data Found");
                }
            }

            var item = await refitApi.CreateBlog(new refitBlog
            {
                BlogAuthor = "ThetPan",
                BlogContent = "HelloThisisContent",
                BlogTitle = "ThisisBlogTitle",
            });

            var item3 = await refitApi.PatchBlog(4, new refitBlog
            {
                BlogAuthor = "ThetPan3",
                BlogContent = "HelloThisisContent3",
                BlogTitle = "ThisisBlogTitle3"
            });

            var item4 = await refitApi.DeleteBlog(4);

        }
    }
}
