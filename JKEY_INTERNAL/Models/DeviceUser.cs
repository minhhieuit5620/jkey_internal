using System;
using System.Collections.Generic;

namespace JKEY_INTERNAL.Models;

public partial class DeviceUser
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid DeviceId { get; set; }

    public int Status { get; set; }

    public string AuthenType { get; set; } = null!;

    public DateTime IssueDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public DateTime ActivedDate { get; set; }

    public string? ActiveCode { get; set; }

    public Guid? UserCreated { get; set; }

    public Guid? UserDeleted { get; set; }

    public Guid? UserApproved { get; set; }
}
