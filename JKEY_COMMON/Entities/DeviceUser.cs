using JKEY_COMMON.Enums;
using System;
using System.Collections.Generic;

namespace JKEY_COMMON.Entities;

public partial class DeviceUser
{
    /// <summary>
    /// id device user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// id khách hàng
    /// </summary>
    public Guid CustomerId { get; set; }


    /// <summary>
    /// id thiết bị
    /// </summary>
    public Guid DeviceId { get; set; }

    /// <summary>
    /// trạng thái
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// kiểu bảo mật
    /// </summary>
    public string AuthenType { get; set; } = null!;

    /// <summary>
    /// ngày có hiệu lực
    /// </summary>
    public DateTime IssueDate { get; set; }

    /// <summary>
    ///  ngày tạo
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// ngày approve
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
    /// người aprove
    /// </summary>
    public Guid? UserApproved { get; set; }
}
