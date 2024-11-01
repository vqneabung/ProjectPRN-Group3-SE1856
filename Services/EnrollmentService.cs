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
            throw new NotImplementedException();
        }

        public void Delete(Enrollment entity)
        {
            throw new NotImplementedException();
        }

        public Enrollment? Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Enrollment>? GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Enrollment entity)
        {
            throw new NotImplementedException();
        }
    }
}
