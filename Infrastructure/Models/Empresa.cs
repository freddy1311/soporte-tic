using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Empresa
{
    public long EmprCodigo { get; set; }

    public string? EmprNombre { get; set; }

    public string? EmprRuc { get; set; }

    public string? EmprLogo { get; set; }

    public virtual ICollection<ConfiguracionGeneral> ConfiguracionGenerals { get; set; } = new List<ConfiguracionGeneral>();

    public virtual ICollection<Sucursal> Sucursals { get; set; } = new List<Sucursal>();
}
