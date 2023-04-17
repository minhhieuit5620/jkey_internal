using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JKEY_COMMON.Entities;

public partial class MenuRight
{
    [Key]
    public string MenuId { get; set; } = null!;

    public Guid RoleId { get; set; }
}
