using BussinessObjects;

namespace Repositories
{
    public interface IAssignmentRepository
    {
        void Add(Assignment assigment);
        void Update(Assignment assigment);
        void Delete(Assignment assigment);
        List<Assignment> GetAll();
    }
}