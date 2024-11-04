using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class EnrollmentDAO : IDAO<Enrollment>
    {
        private readonly LmsContext lmsContext;
        public EnrollmentDAO(LmsContext context)
        {
            lmsContext = context;
        }
        public void Add(Enrollment entity)
        {
            try
            {
                lmsContext.Enrollments.Add(entity);
                lmsContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Enrollment entity)
        {
            try
            {
                var enrollment = lmsContext.Enrollments.SingleOrDefault(e => e.EnrollmentId == entity.EnrollmentId);
                if (enrollment != null)
                {
                    lmsContext.Enrollments.Remove(enrollment);
                    lmsContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Enrollment GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Enrollment> GetAll()
        {
            return lmsContext.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .ToList();
        }

        public void Update(Enrollment entity)
        {
        }

        public List<Enrollment> GetEnrollmentsByStudentId(int studentId)
        {
            return lmsContext.Enrollments
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId && e.Status == true) // Assuming Status indicates active enrollments
                .ToList();
        }
    }
}
