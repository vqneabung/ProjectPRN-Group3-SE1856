using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class Submission
{
    public int SubmissionId { get; set; }

    public int? AssignmentId { get; set; }

    public int? StudentId { get; set; }

    public DateOnly? SubmissionDate { get; set; }

    public decimal? Grade { get; set; }

    public virtual Assignment? Assignment { get; set; }
}
