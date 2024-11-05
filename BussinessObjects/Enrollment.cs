using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int? StudentId { get; set; }

    public int? CourseId { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    public bool? Status { get; set; }

    public virtual Course? Course { get; set; }

    public virtual User? Student { get; set; }
}
