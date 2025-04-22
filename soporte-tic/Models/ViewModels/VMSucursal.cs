namespace soporte_tic.Models.ViewModels
{
    public class VMSucursal
    {
        public long SucuCodigo { get; set; }

        public string SucuNombre { get; set; } = null!;

        public string? SucuDireccion { get; set; }

        public string? SucuTelefono { get; set; }

        public string? SucuCiudad { get; set; }

        public string? SucuContacto { get; set; }

        public string? SucuResponsable { get; set; }

        public string? SucuEmail { get; set; }

        public int SucuEstado { get; set; }

        public long EmprCodigo { get; set; }

        public string? EmprNombre { get; set; }
    }
}
