using JKEY_COMMON.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JKEY_COMMON.Entities;

public partial class Menu
{
   /// <summary>
   /// menu id
   /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// tên menu
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// icon menu
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// menu cha
    /// </summary>
    public string ParentId { get; set; } = null!;

    /// <summary>
    /// thứ tự 
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// link
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    /// id page
    /// </summary>
    public string? PageId { get; set; }

    /// <summary>
    /// trạng thái
    /// </summary>
    public bool Active { get; set; }

    public Guid? UserCreated { get; set; }

    public Guid? UserModified { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
