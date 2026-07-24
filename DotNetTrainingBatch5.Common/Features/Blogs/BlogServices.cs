using DotNetTrainingBatch5.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.Common.Features.Blogs
{
    public class BlogServices : IBlogServices
    {
        //protected readonly AppDBContext _db = new AppDBContext();

        private readonly AppDBContext _db;

        public BlogServices(AppDBContext db)
        {
            _db = db;
        }

        public List<TblBlog> getBlogs()
        {
            var data = _db.TblBlogs.AsNoTracking().Where(x=>x.DeleteFlag == false).ToList();
            return data;
        }

        public List<TblBlog> getBlog(int id)
        {
            var data = _db.TblBlogs.AsNoTracking().Where(x => x.BlogId == id && x.DeleteFlag == false).ToList();
            return data;
        }

        public TblBlog createBlog(TblBlog data_blog)
        {
            _db.Add(data_blog);
            _db.SaveChanges();
            return data_blog;
        }

        public TblBlog updateBlog(int id, TblBlog data_blog)
        {
            var itemdata = _db.TblBlogs.AsNoTracking().Where(_ => _.BlogId == id).FirstOrDefault();
            if (itemdata is null) return null;

            itemdata.BlogTitle = data_blog.BlogTitle;
            itemdata.BlogAuthor = data_blog.BlogAuthor;
            itemdata.BlogContent = data_blog.BlogContent;

            _db.Entry(itemdata).State = EntityState.Modified;
            _db.SaveChanges();

            return data_blog;
        }

        public TblBlog patchBlog(int id, TblBlog data_blog)
        {
            var itemdata = _db.TblBlogs.AsNoTracking().FirstOrDefault(_ => _.BlogId == id);
            if (itemdata is null) return null;

            if (data_blog.BlogTitle is not null)
            {
                itemdata.BlogTitle = data_blog.BlogTitle;
            }
            if (data_blog.BlogAuthor is not null)
            {
                itemdata.BlogAuthor = data_blog.BlogAuthor;
            }
            if (data_blog.BlogContent is not null)
            {
                itemdata.BlogContent = data_blog.BlogContent;
            }

            _db.Entry(itemdata).State = EntityState.Modified;
            _db.SaveChanges();

            return data_blog;
        }

        public TblBlog? deleteBlog(int id)
        {
            var itemdata = _db.TblBlogs.AsNoTracking().Where(_ => _.BlogId == id).FirstOrDefault();
            if (itemdata is null) return null;

            itemdata.DeleteFlag = true;

            _db.Entry(itemdata).State = EntityState.Modified;
            _db.SaveChanges();

            return itemdata;
        }
    }
}
