using System;
using System.Collections.Generic;

namespace MuafiyetProjesi2024.Models;

public partial class Evraklar
{
    public int EvrakId { get; set; }

    public string? Transkript { get; set; }

    public string? DersIcerik { get; set; }

    public string? Tckimlik { get; set; }

    public virtual Kullanicilar? TckimlikNavigation { get; set; }
}
