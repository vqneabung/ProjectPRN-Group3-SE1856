using BussinessObjects;

namespace Services
{
    public interface IAssignmentService
    {
        void Add(Assignment assigment);
        void Update(Assignment assigment);
        void Delete(Assignment assigment);
        List<Assignment> GetAll();
    }
}