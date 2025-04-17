using Domain.Utils;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Interface
{
    public interface IUsuarioService
    {
        #region methods
        Task<ResponseModel> GetUsers();

        Task<ResponseModel> GetUsersTecnico();

        Task<ResponseModel> Get(long id);

        Task<ResponseModel> Create(Usuario entity, Stream photo = null, string urlTemplateEmail = "");

        Task<ResponseModel> Update(Usuario entity, Stream photo = null);

        Task<ResponseModel> Delete(long id);

        Task<ResponseModel> CheckLoginUser(string email, string password);

        Task<ResponseModel> ChangePassword(long id, string currentPassword, string newPassword);

        Task<ResponseModel> ResetPassword(long id, string destineEmail, string urlTemplateEmail);
        #endregion
    }
}
