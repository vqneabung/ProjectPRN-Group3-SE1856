using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SubmissionRepository : IRepository<Submission>
    {
        private readonly SubmissionDAO _submissionDAO;

        public SubmissionRepository(SubmissionDAO submissionDAO)
        {
            _submissionDAO = submissionDAO;
        }

        public void Add(Submission entity)
        {
            _submissionDAO.Add(entity);
        }

        public void Delete(Submission entity)
        {
            _submissionDAO.Delete(entity);
        }

        public IEnumerable<Submission>? GetAll()
        {
            return _submissionDAO.GetAll();
        }

        public Submission? GetByID(int id)
        {
            return _submissionDAO.GetByID(id);
        }

        public void Update(Submission entity)
        {
            _submissionDAO.Update(entity);
        }
    }
}
