using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AssignmentService : IService<Assignment>
    {
        private readonly IRepository<Assignment> _repository;

        public AssignmentService(IRepository<Assignment> repository)
        {
            _repository = repository;
        }

        public void Add(Assignment entity)
        {
            _repository.Add(entity);
        }

        public void Delete(Assignment entity)
        {
            _repository.Delete(entity);
        }

        public Assignment? Get(int id)
        {
            return _repository.GetByID(id);
        }

        public IEnumerable<Assignment>? GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(Assignment entity)
        {
            _repository.Update(entity);
        }
    }
}
