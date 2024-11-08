using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService:IService<User>
    {
        private readonly IRepository<User> _service;
        public UserService(IRepository<User> service)
        {
            _service = service;
        }

        public void Add(User entity)
        {
            _service.Add(entity);
        }

        public void Delete(User entity)
        {
            _service.Delete(entity);
        }

        public User? Get(int id)
        {
            return _service.GetByID(id);
        }

        public IEnumerable<User>? GetAll()
        {
            return _service.GetAll();
        }

        public void Update(User entity)
        {
            _service.Update(entity);
        }
    }
}
