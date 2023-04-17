using System;
using System.Collections.Generic;

namespace JKEY_COMMON.Entities;

public partial class UserRole
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }
}
