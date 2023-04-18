using JKEY_INTERNAL.Models.Attributes;
using System;
using System.Collections.Generic;

namespace JKEY_INTERNAL.Models;

public partial class DeviceUser
{
    /// <summary>
    /// id device user
    /// </summary>
    [Primarykey]
    public Guid Id { get; set; }

    /// <summary>
    /// mã khách hàng
    /// </summary>
    [IsNotNullOrEmpty("customer id not null")]
    public Guid CustomerId { get; set; }

    /// <summary>
    /// mã máy
    /// </summary>
    [IsNotNullOrEmpty("device id not null")]
    public Guid DeviceId { get; set; }

    /// <summary>
    /// trạng thái 
    /// </summary>
    [IsNotNullOrEmpty("Status not null")]
    public int Status { get; set; }

    /// <summary>
    /// kiểu bảo mật
    /// </summary>
    [IsNotNullOrEmpty("AuthenType not null")]
    public string AuthenType { get; set; } = null!;

    /// <summary>
    /// ngày có hiệu lực
    /// </summary>
    public DateTime IssueDate { get; set; }

    /// <summary>
    /// ngày tạo
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// ngày chấp thuận
    /// </summary>
    public DateTime? ApprovedDate { get; set; }

    /// <summary>
    /// ngày xóa
    /// </summary>
    public DateTime? DeletedDate { get; set; }

    /// <summary>
    /// ngày active
    /// </summary>
    public DateTime ActivedDate { get; set; }

    /// <summary>
    /// mã active
    /// </summary>
    public string? ActiveCode { get; set; }

    /// <summary>
    /// người tạo
    /// </summary>
    public Guid? UserCreated { get; set; }

    /// <summary>
    /// người xóa
    /// </summary>
    public Guid? UserDeleted { get; set; }

    /// <summary>
    /// người chấp thuận
    /// </summary>
    public Guid? UserApproved { get; set; }
}
