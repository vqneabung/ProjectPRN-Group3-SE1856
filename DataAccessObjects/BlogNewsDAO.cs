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
            _context.BlogNews.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(BlogNews entity)
        {
            _context.BlogNews.Remove(entity);
            _context.SaveChanges();
        }

        public BlogNews Get(int id)
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
           _context.BlogNews.Update(entity);
            _context.SaveChanges();
        }
    }
}
