using System;
using System.Collections.Generic;

namespace JKEY_INTERNAL.Models;

public partial class Menu
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Icon { get; set; }

    public string ParentId { get; set; } = null!;

    public int Order { get; set; }

    public string? Link { get; set; }

    public string? PageId { get; set; }

    public bool Active { get; set; }

    public Guid? UserCreated { get; set; }

    public Guid? UserModified { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
