using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class OrdenTrabajo
{
    public long OrtrCodigo { get; set; }

    public DateTime? OrtrFechaCreacion { get; set; }

    public DateTime? OrtrFechaEmision { get; set; }

    public DateTime? OrtrFechaPrevistaInicio { get; set; }

    public DateTime? OrtrFechaPrevistaFin { get; set; }

    public DateTime? OrtrFechaEjecucionInicio { get; set; }

    public DateTime? OrtrFechaEjecucionFin { get; set; }

    public int? OrtrSemana { get; set; }

    public int? OrtrNúmero { get; set; }

    public int? OrtrTipo { get; set; }

    public long MaquCodigo { get; set; }

    public string? OrtrObservacion { get; set; }

    public long UsuaResponsable { get; set; }

    public long UsuaRevisa { get; set; }

    public long CodtCodigo { get; set; }

    public virtual ConfiguracionOdt CodtCodigoNavigation { get; set; } = null!;

    public virtual ICollection<DetalleOdt> DetalleOdts { get; set; } = new List<DetalleOdt>();

    public virtual Maquinaria MaquCodigoNavigation { get; set; } = null!;

    public virtual Usuario UsuaResponsableNavigation { get; set; } = null!;

    public virtual Usuario UsuaRevisaNavigation { get; set; } = null!;
}
