using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Maquinaria
{
    public long MaquCodigo { get; set; }

    public DateTime? MaquFechaCreacion { get; set; }

    public DateTime? MaquFechaAct { get; set; }

    public string? MaquNombre { get; set; }

    public string? MaquDescripcion { get; set; }

    public int? MaquTipo { get; set; }

    public int? MaquEstado { get; set; }

    public long SucuCodigo { get; set; }

    public long MaquCodigoFk { get; set; }

    public virtual ICollection<Maquinaria> InverseMaquCodigoFkNavigation { get; set; } = new List<Maquinaria>();

    public virtual Maquinaria MaquCodigoFkNavigation { get; set; } = null!;

    public virtual ICollection<OrdenTrabajo> OrdenTrabajos { get; set; } = new List<OrdenTrabajo>();

    public virtual Sucursal SucuCodigoNavigation { get; set; } = null!;

    public virtual ICollection<TareasMaquinaria> TareasMaquinaria { get; set; } = new List<TareasMaquinaria>();
}
