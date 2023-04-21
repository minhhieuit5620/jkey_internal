using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JKEY_INTERNAL.Models;

public partial class User
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "FullName is required")]
    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public int Gender { get; set; }

    public bool Active { get; set; }

    public DateTime DateOfBirth { get; set; }

    public Guid UserCreated { get; set; }

    public Guid UserModified { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }
}
