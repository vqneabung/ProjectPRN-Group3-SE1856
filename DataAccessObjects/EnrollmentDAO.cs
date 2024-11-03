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
            throw new NotImplementedException();
        }

        public void Delete(Enrollment entity)
        {
            throw new NotImplementedException();
        }

        public Enrollment GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Enrollment> GetAll()
        {
            throw new NotImplementedException();
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
