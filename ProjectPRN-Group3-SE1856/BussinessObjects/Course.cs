using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class Course
{
    public int CourseId { get; set; }

    public string? CourseName { get; set; }

    public string? Description { get; set; }

    public int? Credits { get; set; }

    public int? DepartmentId { get; set; }

    public int? CourseTypeId { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual CourseType? CourseType { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Forum> Forums { get; set; } = new List<Forum>();
}
