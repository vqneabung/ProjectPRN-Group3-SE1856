using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class EnrollmentRepository : IRepository<Enrollment>
    {
        private readonly EnrollmentDAO _enrollmentDAO;

        public EnrollmentRepository(EnrollmentDAO enrollmentDAO)
        {
            _enrollmentDAO = enrollmentDAO;
        }

        public void Add(Enrollment entity)
        {
            _enrollmentDAO.Add(entity);
        }

        public void Delete(Enrollment entity)
        {
           _enrollmentDAO.Delete(entity);
        }

        public IEnumerable<Enrollment>? GetAll()
        {
            return _enrollmentDAO.GetAll();
        }

        public Enrollment? GetByID(int id)
        {
            return _enrollmentDAO.GetByID(id);
        }

        public void Update(Enrollment entity)
        {
            _enrollmentDAO.Update(entity);
        }
    }
}
