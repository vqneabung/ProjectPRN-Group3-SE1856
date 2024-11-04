using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class UserDAO : IDAO<User>
    {
        private readonly LmsContext lmsContext;

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }
        public void Update(User entity)
        {
        }


        public IEnumerable<User> GetAll()
        {
            using (var db = new LmsContext())
            {
                return db.Users.ToList();
            }
        }



        public User GetByUserName(string userName)
        {
            using (var db = new LmsContext())
            {
                return db.Users.SingleOrDefault(user => user.UserName == userName);
            }
        }

        public User? GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public int GetNextUserId()
        {
            using (var db = new LmsContext())
            {
                // Get the maximum UserId from the Users table and add 1
                return db.Users.Any() ? db.Users.Max(u => u.UserId) + 1 : 1;
            }
        }
    }
}
