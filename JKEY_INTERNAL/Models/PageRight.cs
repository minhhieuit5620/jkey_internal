using System;
using System.Collections.Generic;

namespace JKEY_INTERNAL.Models;

public partial class PageRight
{
    public string PageId { get; set; } = null!;

    public Guid RoleId { get; set; }
}
