using System;
using System.Collections.Generic;

namespace MuafiyetProjesi2024.Models;

public partial class Kullanicilar
{
    public string? Mail { get; set; }

    public string? Parola { get; set; }

    public string Tckimlik { get; set; } = null!;

    public virtual ICollection<Basvurular> Basvurulars { get; set; } = new List<Basvurular>();

    public virtual ICollection<Dersler> Derslers { get; set; } = new List<Dersler>();

    public virtual ICollection<Evraklar> Evraklars { get; set; } = new List<Evraklar>();
}
