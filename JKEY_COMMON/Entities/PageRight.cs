using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JKEY_COMMON.Entities;

public partial class PageRight
{
    [Key]
    public string PageId { get; set; } = null!;

    public Guid RoleId { get; set; }
}
