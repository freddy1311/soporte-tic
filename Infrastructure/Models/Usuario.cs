using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Usuario
{
    public long UsuaCodigo { get; set; }

    public DateTime? UsuaFechaCreacion { get; set; }

    public DateTime? UsuaFechaAct { get; set; }

    public string UsuaNombre { get; set; } = null!;

    public string UsuaCedula { get; set; } = null!;

    public string? UsuaTelefono { get; set; }

    public int? UsuaPerfil { get; set; }

    public string? UsuaEmail { get; set; }

    public string? UsuaPassword { get; set; }

    public int? UsuaTipo { get; set; }

    public int? UsuaEstado { get; set; }

    public string? UsuaFoto { get; set; }

    public long SucuCodigo { get; set; }

    public virtual ICollection<OrdenTrabajo> OrdenTrabajoUsuaResponsableNavigations { get; set; } = new List<OrdenTrabajo>();

    public virtual ICollection<OrdenTrabajo> OrdenTrabajoUsuaRevisaNavigations { get; set; } = new List<OrdenTrabajo>();

    public virtual Sucursal SucuCodigoNavigation { get; set; } = null!;
}
