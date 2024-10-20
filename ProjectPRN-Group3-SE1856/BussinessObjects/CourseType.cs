using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class CourseType
{
    public int CourseTypeId { get; set; }

    public string? CourseTypeName { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
