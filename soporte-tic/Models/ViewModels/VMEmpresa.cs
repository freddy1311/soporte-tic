namespace soporte_tic.Models.ViewModels
{
    public class VMEmpresa
    {
        public long EmprCodigo { get; set; }
        public string? EmprNombre { get; set; }
        public string? EmprRuc { get; set; }
        public string? EmprLogo { get; set; }
        public IFormFile? File { get; set; }
    }
}
