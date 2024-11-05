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
        private readonly LmsContext _context;

        public EnrollmentDAO(LmsContext context)
        {
            _context = context;
        }

        public void Add(Enrollment entity)
        {
            entity.EnrollmentId = _context.Enrollments.Max(e => e.EnrollmentId) + 1;
            _context.Enrollments.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Enrollment entity)
        {
            var enrollment = _context.Enrollments.Find(entity.EnrollmentId);
            enrollment.Status = false;
            _context.Enrollments.Update(enrollment);
            _context.SaveChanges();
        }

        public Enrollment GetByID(int id)
        {
            var enrollment = _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefault(e => e.EnrollmentId == id);

            return enrollment;
        }

        public IEnumerable<Enrollment> GetAll()
        {
            if ( _context.Enrollments.ToList() != null)
            {
                return _context.Enrollments
                    .Include(e => e.Course)
                       .Include(e => e.Student)
                    .ToList();
            };
            return null;
        }

        public void Update(Enrollment entity)
        {
            var enrollment = _context.Enrollments.Find(entity.EnrollmentId);
            if (enrollment == null)
            {
                return;
            }

            enrollment.StudentId = entity.StudentId;
            enrollment.CourseId = entity.CourseId;
            enrollment.EnrollmentDate = entity.EnrollmentDate;

            _context.Enrollments.Update(enrollment);
            _context.SaveChanges();
        }
    }
}
