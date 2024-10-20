using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class BlogNews
{
    public int PostId { get; set; }

    public int? UserId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateOnly? PostDate { get; set; }

    public string? Category { get; set; }

    public virtual User? User { get; set; }
}
