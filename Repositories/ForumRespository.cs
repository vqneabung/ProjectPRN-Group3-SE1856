using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ForumRespository : IRepository<Forum>
    {
        private readonly ForumDAO _forumDAO;

        public ForumRespository(ForumDAO forumDAO)
        {
            _forumDAO = forumDAO;
        }

        public void Add(Forum entity)
        {
            _forumDAO.Add(entity);
        }

        public void Delete(Forum entity)
        {
            _forumDAO.Delete(entity);
        }

        public Forum? GetByID(int id)
        {
           return _forumDAO.GetByID(id);
        }

        public IEnumerable<Forum> GetAll()
        {
            return _forumDAO.GetAll();
        }

        public void Update(Forum entity)
        {
           _forumDAO.Update(entity);
        }
    }

}
