using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IEnrollmentService : IService<Enrollment>
    {
        IEnumerable<Enrollment>? GetEnrollmentByStudentId(int studentId);
    }
}
