using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class BlogNewsDAO : IDAO<BlogNews>
    {
        private readonly LmsContext _context;

        public BlogNewsDAO(LmsContext context)
        {
            _context = context;
        }

        public void Add(BlogNews entity)
        {            
            entity.PostId = _context.BlogNews.Max(bl => bl.PostId) + 1;

            _context.BlogNews.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(BlogNews entity)
        {
            _context.BlogNews.Remove(entity);
            _context.SaveChanges();
        }

        public BlogNews GetByID(int id)
        {
           var blogNews = _context.BlogNews.Find(id);
            if (blogNews == null)
            {
                return null;
            }
            else
            {
                return blogNews;
            }
        }

        public IEnumerable<BlogNews> GetAll()
        {
            var blogNews = _context.BlogNews.ToList();
            if (blogNews == null)
            {
                return null;
            }
            else
            {
                return blogNews;
            }
        }

        public void Update(BlogNews entity)
        {
            var blogNews = _context.BlogNews.Find(entity.PostId);

            if (blogNews == null)
            {
                return;
            }

            blogNews.Title = entity.Title;
            blogNews.Content = entity.Content;
            blogNews.Category = entity.Category;

            _context.BlogNews.Update(blogNews);
            _context.SaveChanges();
        }
    }
}
