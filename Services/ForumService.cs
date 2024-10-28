using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ForumService : IService<Forum>
    {
        private readonly IRepository<Forum> _repository;    

        public ForumService(IRepository<Forum> repository)
        {
            _repository = repository;
        }

        public void Add(Forum entity)
        {
            _repository.Add(entity);
        }

        public void Delete(Forum entity)
        {
            _repository.Delete(entity);
        }

        public Forum? Get(int id)
        {
            var forum = _repository.GetByID(id);
            return forum;
        }

        public IEnumerable<Forum>? GetAll()
        {
            var forums = _repository.GetAll();
            return forums;
        }

        public void Update(Forum entity)
        {
            _repository.Update(entity);
        }
    }
}
