using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CourseTypeRepository:IRepository<CourseType>
    {
        private readonly CourseTypeDAO courseTypeDAO;
        public CourseTypeRepository(CourseTypeDAO DAO)
        {
            courseTypeDAO = DAO;
        }
        public void Add(CourseType entity)
        {
            courseTypeDAO.Add(entity);
        }
        public void Update(CourseType entity)
        {
            courseTypeDAO.Update(entity);
        }
        public void Delete(CourseType entity)
        {
            courseTypeDAO.Delete(entity);
        }
        public CourseType? GetByID(int id)
        {
            return courseTypeDAO.GetByID(id);
        }
        public IEnumerable<CourseType>? GetAll()
        {
            return courseTypeDAO.GetAll();
        }
    }
}
