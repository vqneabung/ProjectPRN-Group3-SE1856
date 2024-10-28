using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class DepartmentDAO : IDAO<Department>
    {
        private readonly LmsContext lmsContext;
        public DepartmentDAO (LmsContext context)
        {
            lmsContext=context;
        }
        public void Add(Department entity)
        {
            try
            {
                lmsContext.Departments.Add(entity);
                lmsContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Department entity)
        {
            try
            {
                lmsContext.Entry<Department>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                lmsContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(Department entity)
        {
            try
            {
                var temp = lmsContext.Courses.SingleOrDefault(c => c.DepartmentId == entity.DepartmentId);
                lmsContext.Departments.Remove(temp);
                lmsContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Department Get(int id)
        {
            return lmsContext.Departments.FirstOrDefault(c => c.DepartmentId == id);
        }

        public IEnumerable<Department> GetAll()
        {
            IEnumerable<Department> list = new List<Department>();
            try
            {
                list = lmsContext.Departments.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

    }
}
