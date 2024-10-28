using BussinessObjects;
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
            var Assignments = _context.Assignments.ToList();
            return Assignments != null && Assignments.Any() ? Assignments : null;
        }

        public void Update(Assignment assigment)
        {
            try
            {
                _context.Update(assigment);
                _context.SaveChanges();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
