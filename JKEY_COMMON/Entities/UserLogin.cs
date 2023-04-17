using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JKEY_COMMON.Entities;

public partial class UserLogin
{
    [Key]
    public string ProviderKey { get; set; } = null!;

    public Guid UserId { get; set; }

    public string? LoginProvider { get; set; }

    public string? ProviderDisplayName { get; set; }
}
