using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class DetalleOdt
{
    public long DodtCodigo { get; set; }

    public DateTime? DodtFecha { get; set; }

    public long? OrtrCodigo { get; set; }

    public long? TamaCodigo { get; set; }

    public int? DodtResultado { get; set; }

    public string? DodtObservacion { get; set; }

    public virtual OrdenTrabajo? OrtrCodigoNavigation { get; set; }

    public virtual TareasMaquinaria? TamaCodigoNavigation { get; set; }
}
