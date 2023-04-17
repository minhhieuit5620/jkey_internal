using System;
using System.Collections.Generic;

namespace JKEY_INTERNAL.Models;

public partial class MenuRight
{
    public string MenuId { get; set; } = null!;

    public Guid RoleId { get; set; }
}
