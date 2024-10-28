using BussinessObjects;
using DataAccessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DepartmentService : IService<Department>
    {
        private readonly IRepository<Department> departmentRepository;
        public DepartmentService(IRepository<Department> repository)
        {
            departmentRepository = repository;
        }
        public void Add(Department entity)
        {
            departmentRepository.Add(entity);
        }
        public void Update(Department entity)
        {
            departmentRepository.Update(entity);
        }
        public void Delete(Department entity)
        {
            departmentRepository.Delete(entity);
        }
        public Department? Get(int id)
        {
            return departmentRepository.GetByID(id);
        }
        public IEnumerable<Department>? GetAll()
        {
            return departmentRepository.GetAll();
        }
    }
}
