using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class DocumentDAO : IDAO<Document>
    {
        private readonly LmsContext lmsContext;
        public DocumentDAO(LmsContext context)
        {
            lmsContext = context;
        }
        public void Add(Document entity)
        {
            try
            {
                lmsContext.Documents.Add(entity);
                lmsContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Document entity)
        {
            try
            {
                var trackedEntity = lmsContext.Documents.Local.FirstOrDefault(d => d.DocumentId == entity.DocumentId);

                if (trackedEntity != null)
                {
                    lmsContext.Entry(trackedEntity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                }

                lmsContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                lmsContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Document entity)
        {
            try
            {
                var temp = lmsContext.Documents.SingleOrDefault(c => c.DocumentId == entity.DocumentId);
                lmsContext.Documents.Remove(temp);
                lmsContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Document GetByID(int id)
        {
            return lmsContext.Documents.FirstOrDefault(c => c.DocumentId == id);
        }
        public IEnumerable<Document> GetAll()
        {
            IEnumerable<Document> list = new List<Document>();
            try
            {
                list = lmsContext.Documents.Include(d => d.Course).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
