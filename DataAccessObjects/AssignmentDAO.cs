using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class AssignmentDAO : IDAO<Assignment>
    {
        
        private readonly LmsContext _context;

        public AssignmentDAO(LmsContext context)
        {
            _context = context;
        }
        public void Add(Assignment assigment)
        {
            try
            {
                _context.Assignments.Add(assigment);
                _context.SaveChanges();
            }catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void Delete(Assignment assigment)
        {
            try
            {
                var existingAssignment = _context.Assignments
                                  .Local
                                  .FirstOrDefault(a => a.AssignmentId == assigment.AssignmentId);
                if (existingAssignment != null)
                {
                    _context.Entry(existingAssignment).State = EntityState.Detached;
                }

                _context.Remove(assigment);
                _context.SaveChanges();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Assignment GetByID(int id) 
        {
            Assignment assignment = _context.Assignments.FirstOrDefault(a => a.AssignmentId == id);
            return assignment !=null ? assignment : null;
        }

        public IEnumerable<Assignment> GetAll()
        {
            var Assignments = _context.Assignments
                .Include(a => a.Class)
                .Include(a => a.Submissions)
                .OrderByDescending(a => a.AssignmentId)
                .ToList();

            return Assignments != null && Assignments.Any() ? Assignments : null;
        }

        public void Update(Assignment assigment)
        {
            try
            {
                var existingAssignment = _context.Assignments
                                  .Local
                                  .FirstOrDefault(a => a.AssignmentId == assigment.AssignmentId);
                if (existingAssignment != null)
                {
                    _context.Entry(existingAssignment).State = EntityState.Detached;
                }


                _context.Assignments.Update(assigment);
                _context.SaveChanges();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
