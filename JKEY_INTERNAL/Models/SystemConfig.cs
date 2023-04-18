using JKEY_INTERNAL.Models.Attributes;
using System;
using System.Collections.Generic;

namespace JKEY_INTERNAL.Models;

public partial class SystemConfig
{
    [Primarykey]
    public Guid Id { get; set; }

    /// <summary>
    /// Tên của config
    /// </summary>
    [IsNotNullOrEmpty("Name of system config not null")]
    public string Name { get; set; } 

    /// <summary>
    /// giá trị tên của system config
    /// </summary>
    [IsNotNullOrEmpty("Value Name of system config not null")]
    public string Value { get; set; }
   
    [IsNotNullOrEmpty("Type of system config not null")]
    public string Type { get; set; } 

    [IsNotNullOrEmpty("Status of system config not null")]
    public bool Active { get; set; }

    [IsNotNullOrEmpty("Description of system config not null")]
    public string Description { get; set; }

    public Guid? UserCreated { get; set; }

    public Guid? UserModified { get; set; }

    [IsNotNullOrEmpty("DateCreated of system config not null")]
    public DateTime DateCreated { get; set; }

    [IsNotNullOrEmpty("DateModified of system config not null")]
    public DateTime DateModified { get; set; }
}
