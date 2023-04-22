using System;
using System.Collections.Generic;

namespace JKEY_INTERNAL.Models;

public partial class Role
{
    public Guid Id { get; set; }

    public string? RoleName { get; set; }

    public bool Active { get; set; }

    public Guid UserCreated { get; set; }

    public Guid UserModified { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}
