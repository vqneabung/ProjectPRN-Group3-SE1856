using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class AssignmentDAO
    {
        public static void Add(Assignment assigment)
        {
            
        }

        public static void Delete(Assignment assigment)
        {
            
        }

        public static List<Assignment> GetAll()
        {
            try
            {
                List<Assignment> list = new List<Assignment>();
                using var db = new LmsContext();
                return list = [.. db.Assignments];
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public static void Update(Assignment assigment)
        {
            
        }
    }
}
