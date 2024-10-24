using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BlogNewsRepository : IRepository<BlogNews>
    {
        private readonly BlogNewsDAO _blogNewsDAO;

        public BlogNewsRepository(BlogNewsDAO blogNewsDAO)
        {
            _blogNewsDAO = blogNewsDAO;
        }

        public void Add(BlogNews entity)
        {
            _blogNewsDAO.Add(entity);
        }

        public void Delete(BlogNews entity)
        {
           _blogNewsDAO.Delete(entity);
        }

        public BlogNews? Get(int id)
        {
            return _blogNewsDAO.Get(id);
        }

        public IEnumerable<BlogNews>? GetAll()
        {
            return _blogNewsDAO.GetAll();
        }

        public void Update(BlogNews entity)
        {
           _blogNewsDAO.Update(entity);
        }
    }
}
