namespace soporte_tic.Models.ViewModels
{
    public class VMOrdenTrabajo
    {
        #region properties
        public long OrtrCodigo { get; set; }
        public DateTime? OrtrFechaPrevistaInicio { get; set; }
        public DateTime? OrtrFechaPrevistaFin { get; set; }
        public DateTime? OrtrFechaEjecucionInicio { get; set; }
        public DateTime? OrtrFechaEjecucionFin { get; set; }
        public int? OrtrSemana { get; set; }
        public int? OrtrNumero { get; set; }
        public int? OrtrTipo { get; set; }
        public long MaquCodigoF { get; set; }
        public string? MaquNombreF { get; set; }
        public long MaquCodigo { get; set; }
        public string? MaquNombre { get; set; }
        public string? OrtrObservacion { get; set; }
        public long? UsuaResponsable { get; set; }
        public string? UsuaResponsableNombre { get; set; }
        public long? UsuaRevisa { get; set; }
        public string? UsuaRevisaNombre { get; set; }
        public long CodtCodigo { get; set; }
        #endregion
    }
}
