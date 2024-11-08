using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository:IRepository<User>
    {
        private readonly UserDAO userDAO;
        public UserRepository(UserDAO DAO)
        {
            userDAO = DAO;
        }

        public void Add(User entity)
        {
            userDAO.Add(entity);
        }

        public void Delete(User entity)
        {
            userDAO.Delete(entity);
        }

        public IEnumerable<User>? GetAll()
        {
            return userDAO.GetAll();
        }

        public User? GetByID(int id)
        {
            return (userDAO.GetByID(id));
        }

        public void Update(User entity)
        {
            userDAO.Update(entity);
        }
    }
}
