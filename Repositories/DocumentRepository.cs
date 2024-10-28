using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DocumentRepository:IRepository<Document>
    {
        private readonly DocumentDAO documentDAO;
        public DocumentRepository(DocumentDAO DAO)
        {
            documentDAO = DAO;
        }
        public void Add(Document entity)
        {
            documentDAO.Add(entity);
        }
        public void Update(Document entity)
        {
            documentDAO.Update(entity);
        }
        public void Delete(Document entity)
        {
            documentDAO.Delete(entity);
        }

        public Document? GetByID(int id)
        {
            return documentDAO.GetByID(id);
        }

        public IEnumerable<Document>? GetAll()
        {
            return documentDAO.GetAll();
        }
    }
}
