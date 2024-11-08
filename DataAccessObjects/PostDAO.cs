using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class PostDAO : IDAO<Post>
    {
        private readonly LmsContext _context;

        public PostDAO(LmsContext context)
        {
            _context = context;
        }

        public void Add(Post entity)
        {
            try
            {

                entity.PostId = _context.Posts.Max(p => p.PostId) + 1;
                _context.Posts.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Post entity)
        {
            try
            {
                _context.Entry<Post>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Post entity)
        {
            try
            {
                var temp = _context.Posts.SingleOrDefault(p => p.PostId == entity.PostId);
                _context.Remove(temp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Post GetByID(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.PostId == id);
        }

        public IEnumerable<Post> GetAll()
        {
            IEnumerable<Post> list = new List<Post>();
            try
            {
                list = _context.Posts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
