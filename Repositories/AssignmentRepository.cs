using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        public void Add(Assignment assigment)
            => AssignmentDAO.Add(assigment);

        public void Delete(Assignment assigment)
            => AssignmentDAO.Delete(assigment);

        public List<Assignment> GetAll()
            => AssignmentDAO.GetAll();

        public void Update(Assignment assigment)
            => AssignmentDAO.Update(assigment);
    }
}
