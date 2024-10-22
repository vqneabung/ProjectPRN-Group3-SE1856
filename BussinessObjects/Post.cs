using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class Post
{
    public int PostId { get; set; }

    public int? ForumId { get; set; }

    public int? UserId { get; set; }

    public string? Content { get; set; }

    public DateOnly? PostDate { get; set; }

    public virtual Forum? Forum { get; set; }

    public virtual User? User { get; set; }
}
