namespace soporte_tic.Models.ViewModels
{
    public class VMMaquinariaTarea
    {
        #region propiedades
        public long TamaCodigo { get; set; }
        public string? TamaNombre { get; set; }
        public string? TamaDescripcion { get; set; }
        public int? TamaEstado { get; set; }
        public long MaquCodigo { get; set; }
        public string? MaquNombre { get; set; }
        #endregion
    }
}
