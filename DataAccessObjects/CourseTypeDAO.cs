using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CourseTypeDAO : IDAO<CourseType>
    {
        private readonly LmsContext _context;

        public CourseTypeDAO(LmsContext context)
        {
            _context = context;
        }

        public void Add(CourseType entity)
        {
            _context.CourseTypes.Add(entity); // Add the course type to the context
            _context.SaveChanges(); // Save changes to the database
        }

        public void Delete(CourseType entity)
        {
            var courseType = _context.CourseTypes.Find(entity.CourseTypeId); // Find the entity by ID
            if (courseType != null)
            {
                _context.CourseTypes.Remove(courseType); // Remove the entity
                _context.SaveChanges(); // Save changes to the database
            }
        }

        public CourseType GetByID(int id)
        {
            return _context.CourseTypes.Find(id); // Find and return the entity by ID
        }

        public IEnumerable<CourseType> GetAll()
        {
            return _context.CourseTypes.ToList(); // Return all course types as a list
        }

        public void Update(CourseType entity)
        {
            var existingCourseType = _context.CourseTypes.Find(entity.CourseTypeId); // Find the existing entity
            if (existingCourseType != null)
            {
                existingCourseType.CourseTypeName = entity.CourseTypeName; // Update the properties
                _context.SaveChanges(); // Save changes to the database
            }
        }

    }
}
