using Domain.Utils;

namespace soporte_tic.Services.LocalStorage
{
    public class LocalFileService : ILocalFileService
    {
        private readonly IWebHostEnvironment _env;
        private const string UploadsFolder = "uploads";

        public LocalFileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public ResponseModel DeleteImageAsync(string filePath)
        {
            var rm = new ResponseModel();

            try
            {
                // Valida que sea una ruta permitida
                if (!filePath.StartsWith($"/{UploadsFolder}/"))
                    rm.SetResponse(false, "Ruta no permitida", "Eliminar imagen");

                var fullPath = Path.Combine(_env.WebRootPath, filePath.TrimStart('/'));

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    rm.SetResponse(true, $"Recurso eliminado correctamente!.", "Eliminar imagen");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error en DeleteImageAsync: {ex.Message}", "Eliminar imagen");
            }

            return rm;
        }

        public async Task<ResponseModel> SaveImageAsync(Stream fileStream, string fileName)
        {
            var rm = new ResponseModel();

            try
            {
                // Genera un nombre único
                var uniqueFileName = $"{Guid.NewGuid()}-{Path.GetFileName(fileName)}";
                var uploadsPath = Path.Combine(_env.WebRootPath, UploadsFolder);

                // Crea la carpeta si no existe
                Directory.CreateDirectory(uploadsPath);

                var fullPath = Path.Combine(uploadsPath, uniqueFileName);

                // Guarda el archivo
                using (var file = new FileStream(fullPath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(file);
                }

                var urlImage = $"/{UploadsFolder}/{uniqueFileName}"; // Ruta relativa
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error en SaveImageAsync: ${ex.Message}", "Registro de imagen");
            }

            return rm;
        }
    }
}
