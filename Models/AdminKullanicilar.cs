﻿using System;
using System.Collections.Generic;

namespace MuafiyetProjesi2024.Models;

public partial class AdminKullanicilar
{
    public string? UserName { get; set; }

    public string Tckimlik { get; set; } = null!;

    public string? Parola { get; set; }
}
