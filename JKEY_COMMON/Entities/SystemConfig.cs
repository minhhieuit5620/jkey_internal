using JKEY_COMMON.Enums;
using System;
using System.Collections.Generic;

namespace JKEY_COMMON.Entities;

public partial class SystemConfig
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool Active { get; set; }

    public string Description { get; set; } = null!;

    public Guid? UserCreated { get; set; }

    public Guid? UserModified { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}
