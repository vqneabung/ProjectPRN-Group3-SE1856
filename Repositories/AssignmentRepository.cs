using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AssignmentRepository : IRepository<Assignment>
    {
        private readonly AssignmentDAO _assignmentDAO;

        public AssignmentRepository(AssignmentDAO assignmentDAO)
        {
            _assignmentDAO = assignmentDAO;
        }

        public void Add(Assignment entity)
        {
            _assignmentDAO.Add(entity);
        }

        public void Delete(Assignment entity)
        {
            _assignmentDAO.Delete(entity);
        }

        public Assignment? GetByID(int id)
            => _assignmentDAO.GetByID(id);

        public IEnumerable<Assignment>? GetAll()
            => _assignmentDAO.GetAll();

        public void Update(Assignment entity)
        {
            _assignmentDAO.Update(entity);
        }
    }
}
