using System;
using System.Collections.Generic;

namespace MuafiyetProjesi2024.Models;

public partial class Dersler
{
    public int DersId { get; set; }

    public string? Tckimlik { get; set; }

    public string? OncekiDersAdi { get; set; }

    public int? OncekiDersKredisi { get; set; }

    public int? OncekiDersAkts { get; set; }

    public string? MuafDersAdi { get; set; }

    public int? MuafDersKredisi { get; set; }

    public int? MuafDersAkts { get; set; }

    public virtual Kullanicilar? TckimlikNavigation { get; set; }
}
