using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class ForumDAO : IDAO<Forum>
    {
        private readonly LmsContext _context;
        public ForumDAO(LmsContext context)
        {
            _context = context;
        }


        public void Add(Forum entity)
        {
            _context.Forums.Add(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Forum entity)
        {
            _context.Forums.Remove(entity);
            _context.SaveChanges();
        }

        public Forum? Get(int id)
        {
            var forum = _context.Forums.Find(id);
            if (forum == null)
            {
                return null;

            } else
            {
                return forum;
            }
        }

        public IEnumerable<Forum> GetAll()
        {
            var forums = _context.Forums.ToList();
            if (forums == null)
            {
                return null;
            } else
            {
                return forums;
            }
        }

        public void Update(Forum entity)
        {
            _context.Forums.Update(entity);
            _context.SaveChanges();
        }
    }
}
