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
    public class CourseTypeService:IService<CourseType>
    {
        private readonly IRepository<CourseType> TypeRepository;
        public CourseTypeService(IRepository<CourseType> repository)
        {
            TypeRepository = repository;
        }
        public void Add(CourseType entity)
        {
            TypeRepository.Add(entity);
        }
        public void Update(CourseType entity)
        {
            TypeRepository.Update(entity);
        }
        public void Delete(CourseType entity)
        {
            TypeRepository.Delete(entity);
        }
        public CourseType? Get(int id)
        {
            return TypeRepository.GetByID(id);
        }
        public IEnumerable<CourseType>? GetAll()
        {
            return TypeRepository.GetAll();
        }
    }
}
