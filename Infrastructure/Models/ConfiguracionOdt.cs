using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class ConfiguracionOdt
{
    public long CodtCodigo { get; set; }

    public DateTime? CodtFecha { get; set; }

    public string? CodtVersion { get; set; }

    public string? CodtId { get; set; }

    public int? CodtEstado { get; set; }

    public string? CodtObservacion { get; set; }

    public virtual ICollection<OrdenTrabajo> OrdenTrabajos { get; set; } = new List<OrdenTrabajo>();
}
