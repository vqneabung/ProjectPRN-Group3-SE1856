using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SubmissionService : IService<Submission>
    {
        private readonly IRepository<Submission> _submissionRepository;
        public SubmissionService(IRepository<Submission> repository) 
        {
            _submissionRepository = repository;
        }
        public void Add(Submission entity)
        {
            _submissionRepository.Add(entity);
        }

        public void Delete(Submission entity)
        {
            _submissionRepository?.Delete(entity);
        }
        public IEnumerable<Submission>? GetAll()
        {
            return _submissionRepository.GetAll();
        }

        public void Update(Submission entity)
        {
            _submissionRepository.Update(entity);
        }

        public Submission? Get(int id)
        {
            return _submissionRepository.GetByID(id);
        }
    }
}
