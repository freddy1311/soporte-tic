namespace soporte_tic.Models.ViewModels
{
    public class VMUsuario
    {
        public long UsuaCodigo { get; set; }

        public string UsuaNombre { get; set; } = null!;

        public string UsuaCedula { get; set; } = null!;

        public string? UsuaTelefono { get; set; }

        public int UsuaPerfil { get; set; }

        public string? UsuaEmail { get; set; }

        public string? UsuaPassword { get; set; }

        public int? UsuaTipo { get; set; }

        public int UsuaEstado { get; set; }

        public string? UsuaFoto { get; set; }

        public long SucuCodigo { get; set; }

        public string? SucuNombre { get; set; }

        public IFormFile? File { get; set; }
    }
}
