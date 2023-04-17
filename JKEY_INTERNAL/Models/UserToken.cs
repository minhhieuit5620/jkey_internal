using System;
using System.Collections.Generic;

namespace JKEY_INTERNAL.Models;

public partial class UserToken
{
    public Guid UserId { get; set; }

    public string LoginProvider { get; set; } = null!;

    public string? Name { get; set; }

    public string? Value { get; set; }
}
