using System;
using System.Collections.Generic;

namespace JKEY_INTERNAL.Models;

public partial class UserLogin
{
    public string ProviderKey { get; set; } = null!;

    public Guid UserId { get; set; }

    public string? LoginProvider { get; set; }

    public string? ProviderDisplayName { get; set; }
}
