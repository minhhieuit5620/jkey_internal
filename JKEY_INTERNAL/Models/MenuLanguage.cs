using System;
using System.Collections.Generic;

namespace JKEY_INTERNAL.Models;

public partial class MenuLanguage
{
    public string MenuId { get; set; } = null!;

    public int LangId { get; set; }

    public string Value { get; set; } = null!;
}
