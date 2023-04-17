using System;
using System.Collections.Generic;

namespace JKEY_INTERNAL.Models;

public partial class UserRole
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }
}
