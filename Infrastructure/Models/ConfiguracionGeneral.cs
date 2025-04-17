using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class ConfiguracionGeneral
{
    public long CogeCodigo { get; set; }

    public DateTime? CogeFecha { get; set; }

    public string? CogeKey { get; set; }

    public string? CogeValue { get; set; }

    public string? CogeService { get; set; }

    public string? CogeObservacion { get; set; }

    public long? EmprCodigo { get; set; }

    public virtual Empresa? EmprCodigoNavigation { get; set; }
}
