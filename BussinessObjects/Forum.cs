using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class Forum
{
    public int ForumId { get; set; }

    public int? CourseId { get; set; }

    public string? Title { get; set; }

    public DateOnly? CreateDate { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
