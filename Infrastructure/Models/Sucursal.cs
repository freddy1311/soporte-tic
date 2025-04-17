using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Sucursal
{
    public long SucuCodigo { get; set; }

    public DateTime? SucuFechaCreacion { get; set; }

    public DateTime? SucuFechaAct { get; set; }

    public string? SucuNombre { get; set; }

    public string? SucuCiudad { get; set; }

    public string? SucuDireccion { get; set; }

    public string? SucuTelefono { get; set; }

    public string? SucuEmail { get; set; }

    public string? SucuResponsable { get; set; }

    public int? SucuEstado { get; set; }

    public long EmprCodigo { get; set; }

    public virtual Empresa EmprCodigoNavigation { get; set; } = null!;

    public virtual ICollection<Maquinaria> Maquinaria { get; set; } = new List<Maquinaria>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
