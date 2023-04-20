using JKEY_INTERNAL.Models.Attributes;
using System;
using System.Collections.Generic;

namespace JKEY_INTERNAL.Models;

public partial class User
{
    [Primarykey]
    public Guid Id { get; set; }

    public string FullName { get; set; } 

    public int Gender { get; set; }

    public bool Active { get; set; }

    public string UserName { get; set; } 

    public string Password { get; set; }

    public string? ConfirmPassWord { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public Guid? UserCreated { get; set; }

    public Guid? UserModified { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public string? Address { get; set; }
}
