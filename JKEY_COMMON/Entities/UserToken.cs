using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JKEY_COMMON.Entities;

public partial class UserToken
{
    [Key]
    public Guid UserId { get; set; }

    public string LoginProvider { get; set; } = null!;

    public string? Name { get; set; }

    public string? Value { get; set; }
}
