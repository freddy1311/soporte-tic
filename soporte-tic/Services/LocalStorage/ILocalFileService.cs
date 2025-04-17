using Domain.Utils;

namespace soporte_tic.Services.LocalStorage
{
    public interface ILocalFileService
    {
        #region methods
        Task<ResponseModel> SaveImageAsync(Stream fileStream, string fileName);
        ResponseModel DeleteImageAsync(string filePath);
        #endregion
    }
}
