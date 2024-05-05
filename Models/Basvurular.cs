using System;
using System.Collections.Generic;

namespace MuafiyetProjesi2024.Models;

public partial class Basvurular
{
    public int BasvuruId { get; set; }

    public string? AdSoyad { get; set; }

    public string? Tckimlik { get; set; }

    public string? OgrNo { get; set; }

    public string? Tel { get; set; }

    public string? Mail { get; set; }

    public string? KayitTur { get; set; }

    public string? GeldigiUni { get; set; }

    public string? GeldigiFak { get; set; }

    public string? GeldigiBolum { get; set; }

    public virtual Kullanicilar? TckimlikNavigation { get; set; }
}
