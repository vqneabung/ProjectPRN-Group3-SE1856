using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DepartmentRepository:IRepository<Department>
    {
        private readonly DepartmentDAO departmentDAO;
        public DepartmentRepository(DepartmentDAO DAO)
        {
            departmentDAO = DAO;
        }
        public void Add(Department entity)
        {
            departmentDAO.Add(entity);
        }
        public void Update(Department entity)
        {
            departmentDAO.Update(entity);
        }
        public void Delete(Department entity)
        {
            departmentDAO.Delete(entity);
        }
        public Department? Get(int id)
        {
            return departmentDAO.Get(id);
        }
        public IEnumerable<Department>? GetAll()
        {
            return departmentDAO.GetAll();
        }
    }
}
