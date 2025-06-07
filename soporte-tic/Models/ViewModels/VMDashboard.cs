namespace soporte_tic.Models.ViewModels
{
    public class VMDashboard
    {
        #region propiedades
        public int? CountUsuarios { get; set; }
        public int? CountLineas { get; set; }
        public int? CountMaquinarias { get; set; }
        public int? CountOrdenesTrabajo { get; set; }
        public List<VMOrdenTrabajo>? Ordenes { get; set; }
        #endregion
    }
}
