using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<BlogNews> BlogNews { get; set; } = new List<BlogNews>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
