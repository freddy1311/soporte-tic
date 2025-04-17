using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class TareasMaquinaria
{
    public long TamaCodigo { get; set; }

    public DateTime? TamaFechaCreacion { get; set; }

    public DateTime? TamaFechaAct { get; set; }

    public string? TamaNombre { get; set; }

    public string? TamaDescripcion { get; set; }

    public int? TamaEstado { get; set; }

    public long MaquCodigo { get; set; }

    public virtual ICollection<DetalleOdt> DetalleOdts { get; set; } = new List<DetalleOdt>();

    public virtual Maquinaria MaquCodigoNavigation { get; set; } = null!;
}
