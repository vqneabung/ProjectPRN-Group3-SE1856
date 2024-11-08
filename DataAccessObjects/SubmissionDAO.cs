using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class SubmissionDAO : IDAO<Submission>
    {
        private readonly LmsContext _context;

        public SubmissionDAO(LmsContext context)
        {
            _context = context;
        }

        public void Add(Submission entity)
        {
            try
            {
                // Kiểm tra và đính kèm nếu chưa có trong DbContext
                var trackedAssignment = _context.Assignments.Local
                    .FirstOrDefault(a => a.AssignmentId == entity.AssignmentId);

                if (trackedAssignment != null)
                {
                    entity.Assignment = trackedAssignment; // Sử dụng thực thể đã được theo dõi
                }
                else
                {
                    _context.Attach(entity.Assignment); // Đính kèm nếu chưa có
                }

                _context.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Submission entity)
        {
            throw new NotImplementedException();
        }

        public Submission GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Submission> GetAll()
        {
            var Submission = _context.Submissions
                .Include(s => s.Assignment)
                .OrderByDescending(a => a.SubmissionId)
                .ToList();

            return Submission != null && Submission.Any() ? Submission : null;
        }

        public void Update(Submission entity)
        {
            try
            {
                var existingSubmission = _context.Submissions
                                  .Local
                                  .FirstOrDefault(s => s.SubmissionId == entity.SubmissionId);
                if (existingSubmission != null)
                {
                    _context.Entry(existingSubmission).State = EntityState.Detached;
                }


                _context.Submissions.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
