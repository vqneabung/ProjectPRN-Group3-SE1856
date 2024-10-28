using BussinessObjects;
using DataAccessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DocumentService:IRepository<Document>
    {
        private readonly IRepository<Document> documentRepository;
        public DocumentService (IRepository<Document> repository)
        {
            documentRepository = repository;
        }
        public void Add(Document entity)
        {
            documentRepository.Add(entity);
        }
        public void Update(Document entity)
        {
            documentRepository.Update(entity);
        }
        public void Delete(Document entity)
        {
            documentRepository.Delete(entity);
        }

        public Document? Get(int id)
        {
            return documentRepository.Get(id);
        }

        public IEnumerable<Document>? GetAll()
        {
            return documentRepository.GetAll();
        }
    }
}
