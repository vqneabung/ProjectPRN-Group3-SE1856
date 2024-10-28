using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _iAssignmentRepository;

        public AssignmentService()
        {
            _iAssignmentRepository = new AssignmentRepository();
        }

        public void Add(Assignment assigment)
            => _iAssignmentRepository.Add(assigment);

        public void Delete(Assignment assigment)
            => _iAssignmentRepository.Delete(assigment);

        public List<Assignment> GetAll()
            => _iAssignmentRepository.GetAll();

        public void Update(Assignment assigment)
            => _iAssignmentRepository.Update(assigment);
    }
}
