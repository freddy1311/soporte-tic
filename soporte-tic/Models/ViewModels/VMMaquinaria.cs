namespace soporte_tic.Models.ViewModels
{
    public class VMMaquinaria
    {
        #region propiedades
        public long MaquCodigo { get; set; }
        public string MaquNombre { get; set; }
        public  string MaquDescripcion { get; set; }
        public int? MaquTipo { get; set; }
        public int? MaquEstado { get; set; }
        public long SucuCodigo { get; set; }
        public long? MaquCodigoFK { get; set; }
        public string MaquNombreFK { get; set; }
        #endregion
    }
}
