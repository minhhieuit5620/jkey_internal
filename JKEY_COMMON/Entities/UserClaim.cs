﻿using System;
using System.Collections.Generic;

namespace JKEY_COMMON.Entities;

public partial class UserClaim
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }
}
