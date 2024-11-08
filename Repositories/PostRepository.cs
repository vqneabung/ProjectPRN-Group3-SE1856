using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
   
    public class PostRepository : IRepository<Post>
    {

        private readonly PostDAO _postDAO;

        public PostRepository(PostDAO postDAO)
        {
            _postDAO = postDAO;
        }

        public void Add(Post entity)
        {
            _postDAO.Add(entity);
        }

        public void Delete(Post entity)
        {
            _postDAO.Delete(entity);
        }

        public IEnumerable<Post>? GetAll()
        {
           return _postDAO.GetAll();
        }

        public Post? GetByID(int id)
        {
            return _postDAO.GetByID(id);
        }

        public void Update(Post entity)
        {
           _postDAO.Update(entity);
        }
    }
}
