using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClassService : IService<Class>
    {
        private readonly IRepository<Class> _repository;
        public ClassService(IRepository<Class> repository) 
        { 
            _repository = repository;
        }
        public void Add(Class entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Class entity)
        {
            throw new NotImplementedException();
        }

        public Class? Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Class>? GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(Class entity)
        {
            throw new NotImplementedException();
        }
    }
}
