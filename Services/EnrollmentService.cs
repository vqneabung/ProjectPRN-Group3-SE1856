using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EnrollmentService : IService<Enrollment>
    {
        private readonly IRepository<Enrollment> _repository;

        public EnrollmentService(IRepository<Enrollment> repository)
        {
            _repository = repository;
        }

        public void Add(Enrollment entity)
        {
            _repository.Add(entity);
        }

        public void Delete(Enrollment entity)
        {
            _repository.Delete(entity);
        }

        public Enrollment? Get(int id)
        {
           return _repository.GetByID(id);        
        }

        public IEnumerable<Enrollment>? GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(Enrollment entity)
        {
            _repository.Update(entity);
        }
    }
}
