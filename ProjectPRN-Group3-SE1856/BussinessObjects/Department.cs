using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public int? HeadId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
