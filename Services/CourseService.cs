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
    public class CourseService:IRepository<Course>
    {
        private readonly IRepository<Course> courseRepository;
        public CourseService(IRepository<Course> repository)
        {
            courseRepository = repository;
        }
        public void Add(Course entity)
        {
            courseRepository.Add(entity);
        }
        public void Update(Course entity)
        {
            courseRepository.Update(entity);
        }
        public void Delete(Course entity)
        {
            courseRepository.Delete(entity);
        }

        public Course? GetByID(int id)
        {
            return courseRepository.GetByID(id);
        }

        public IEnumerable<Course>? GetAll()
        {
            return courseRepository.GetAll();
        }
    }
}
