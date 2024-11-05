using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class Class
{
    public int ClassId { get; set; }

    public int? CourseId { get; set; }

    public string? ClassName { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual Course? Course { get; set; }
}
