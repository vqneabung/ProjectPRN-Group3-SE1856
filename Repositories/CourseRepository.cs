using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CourseRepository:IRepository<Course>
    {
        private readonly CourseDAO courseDAO;
        public CourseRepository (CourseDAO DAO)
        {
            courseDAO = DAO;
        }
        public void Add(Course entity)
        {
            courseDAO.Add(entity);
        }
        public void Update(Course entity)
        {
            courseDAO.Update(entity);
        }
        public void Delete(Course entity)
        {
            courseDAO.Delete(entity);
        }

        public Course? GetByID(int id)
        {
            return courseDAO.GetByID(id);
        }

        public IEnumerable<Course>? GetAll()
        {
            return courseDAO.GetAll();
        }
    }
}
