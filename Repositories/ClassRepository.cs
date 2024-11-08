using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ClassRepository : IRepository<Class>
    {
        private readonly ClassDAO ClassDAO;
        public ClassRepository(ClassDAO dao) 
        {
            ClassDAO = dao;
        }
        public void Add(Class entity)
        {
            ClassDAO.Add(entity);
        }

        public void Delete(Class entity)
        {
            ClassDAO.Delete(entity);
        }

        public IEnumerable<Class>? GetAll()
        {
            return ClassDAO.GetAll();
        }

        public Class? GetByID(int id)
        {
            return ClassDAO.GetByID(id);
        }

        public void Update(Class entity)
        {
            ClassDAO.Update(entity);
        }
    }
}
