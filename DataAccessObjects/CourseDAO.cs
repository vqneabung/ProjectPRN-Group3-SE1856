using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CourseDAO : IDAO<Course>
    {
        private readonly LmsContext lmsContext;
        public CourseDAO (LmsContext context)
        {
            lmsContext = context;
        }
        public void Add(Course entity)
        {
            try
            {
                lmsContext.Courses.Add(entity);
                lmsContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Course entity)
        {
            try
            {
                lmsContext.Entry<Course>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                lmsContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Course entity)
        {
            try
            {
                var temp = lmsContext.Courses.SingleOrDefault(c => c.CourseId == entity.CourseId);
                lmsContext.Remove(temp);
                lmsContext.SaveChanges();
            }
            catch (Exception ex)
            {
            throw new Exception(ex.Message);
            }
        }

        public Course GetByID(int id)
        {
            return lmsContext.Courses.FirstOrDefault(c => c.CourseId == id);
        }

        public IEnumerable<Course> GetAll()
        {
            IEnumerable<Course> list = new List<Course>();
            try
            {
                list = lmsContext.Courses.Include(c => c.CourseType).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
