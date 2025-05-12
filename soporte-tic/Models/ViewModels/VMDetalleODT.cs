namespace soporte_tic.Models.ViewModels
{
    public class VMDetalleODT
    {
        #region properties
        public long DodtCodigo { get; set; }
        public long? OrtrCodigo { get; set; }
        public long? TamaCodigo { get; set; }
        public string? TamaNombre { get; set; }
        public int? DodtResultado { get; set; }
        public string? DodtObservacion { get; set; }
        #endregion
    }
}
