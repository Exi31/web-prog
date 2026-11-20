using System;
using System.Collections.Generic;

namespace NTM_DeThiMau2026.Models;

public partial class TheLoai
{
    public int MaTl { get; set; }

    public string TenTl { get; set; } = null!;

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
