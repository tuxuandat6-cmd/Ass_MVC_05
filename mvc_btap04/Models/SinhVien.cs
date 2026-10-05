using System;
using System.Collections.Generic;

namespace mvc_btap04.Models;

public partial class SinhVien
{
    public int Id { get; set; }

    public string MaSv { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public DateOnly NgaySinh { get; set; }

    public string? Email { get; set; }

    public string? LopHoc { get; set; }
}
