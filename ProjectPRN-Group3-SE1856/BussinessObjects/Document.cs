using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class Document
{
    public int DocumentId { get; set; }

    public int? CourseId { get; set; }

    public string? Title { get; set; }

    public byte[]? FileAttachment { get; set; }

    public string? Description { get; set; }

    public virtual Course? Course { get; set; }
}
