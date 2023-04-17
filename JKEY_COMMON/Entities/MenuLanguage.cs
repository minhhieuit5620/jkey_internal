using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JKEY_COMMON.Entities;

public partial class MenuLanguage
{
    [Key]
    public string MenuId { get; set; } = null!;

    public int LangId { get; set; }

    public string Value { get; set; } = null!;
}
