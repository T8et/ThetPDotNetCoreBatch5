using DotNetTrainingBatch5.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDBContext db = new AppDBContext();
            var list = db.Blogs.Where(x=>x.DeleteFlag == false).ToList();
            foreach (var item in list)
            {
                Console.WriteLine(item.BlogId + "-" + item.BlogTitle + "-" + item.BlogAuthor + "-" + item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)
        {
            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            AppDBContext app = new AppDBContext();
            app.Blogs.Add(blog);
            var result = app.SaveChanges();

            Console.WriteLine(result == 1 ? "Inserted Successfully":"Failed Insert");
        }

        public void Edit(int id)
        {
            //BlogModel blog = new BlogModel() { BlogId = id };
            AppDBContext app = new AppDBContext();
            var list = app.Blogs.Where(x=>x.BlogId == id).ToList().FirstOrDefault();

            if (list is null)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            Console.WriteLine(list.BlogId+"-"+list.BlogTitle+"-"+list.BlogAuthor+"-"+list.BlogContent);
        }

        public void Update(int id,string title, string author, string content)
        {
            BlogModel blog = new BlogModel() { BlogTitle = title, BlogAuthor = author, BlogContent = content};
            AppDBContext db = new AppDBContext();
            var result = db.Blogs.AsNoTracking().Where(x => x.BlogId == id).ToList().FirstOrDefault();

            if (result is null) { Console.WriteLine("No data found"); return; }

            if (!string.IsNullOrEmpty(title))
            {
                result.BlogTitle = title;
            }

            if (!string.IsNullOrEmpty(author))
            {
                result.BlogAuthor = author;
            }

            if (!string.IsNullOrEmpty(content))
            {
                result.BlogContent = content;
            }

            db.Entry(result).State = EntityState.Modified;
            var res = db.SaveChanges();

            Console.WriteLine(res == 1 ? "Update Success":"Fail Update");
        }

        public void Delete(int id)
        {
            AppDBContext app = new AppDBContext();
            var result = app.Blogs.AsNoTracking().Where(x => x.BlogId == id).ToList().FirstOrDefault();

            if (result is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            app.Entry(result).State = EntityState.Deleted;
            var res = app.SaveChanges();
            Console.WriteLine(res == 1 ? "Deleted Successfully":"Failed Delete");
        }
    }   
}
