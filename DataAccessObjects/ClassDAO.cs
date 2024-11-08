using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class ClassDAO : IDAO<Class>
    {
        private readonly LmsContext lmsContext;

        public ClassDAO(LmsContext context)
        {
            this.lmsContext = context;
        }

        public void Add(Class entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Class entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Class>? GetAll()
        {
            IEnumerable<Class> list = new List<Class>();
            try
            {
                list = lmsContext.Classes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Class? GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Class entity)
        {
            throw new NotImplementedException();
        }
    }
}
