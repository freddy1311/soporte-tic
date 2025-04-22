using Domain.Business.Interface;
using Domain.Utils.Email;
using Domain.Utils.Encryptation;
using Domain.Utils.Utils;
using Infrastructure.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Business.Implementation
{
    public class UsuarioService: IUsuarioService
    {
        #region properties
        private readonly IGenericRepository<Usuario> _context;
        private readonly IEmailService _emailService;
        private readonly IUtilsService _utilsService;
        private readonly IEncryptionService _encryptionService;
        #endregion

        #region constructor
        public UsuarioService(
            IGenericRepository<Usuario> context,
            IEmailService emailService,
            IUtilsService utilsService,
            IEncryptionService encryptionService
            )
        {
            _context = context;
            _emailService = emailService;
            _utilsService = utilsService;
            _encryptionService = encryptionService;
        }
        #endregion

        #region methods

        public async Task<Utils.ResponseModel> GetUsers()
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _context.GetAll(u => u.UsuaEstado == 1);
                IQueryable<Usuario> query = (IQueryable<Usuario>)rmQuery.Result;


                if (query.ToList().Count > 0)
                {
                    var users = query.
                    OrderBy(u => u.UsuaNombre).
                    ToList();
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Usuarios", users);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo lista de usuarios!.", "Usuarios");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de usuarios: {ex.Message}.", "Usuarios");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> GetUsersTecnico()
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _context.GetAll(u => u.UsuaEstado == 1 && u.UsuaPerfil == 5);
                IQueryable<Usuario> query = (IQueryable<Usuario>)rmQuery.Result;

                if (query.ToList().Count > 0)
                {
                    var users = query.
                    OrderBy(u => u.UsuaNombre).
                    ToList();
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Usuarios", users);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo lista de usuarios!.", "Usuarios");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(true, $"No se pudo obtener la lista de usuarios técnicos: {ex.Message}.", "Usuarios");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Get(long id)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmQuery = await _context.Get(u => u.UsuaCodigo == id);
                IQueryable<Usuario> queryUser = rmQuery.Result;

                Usuario user = queryUser.FirstOrDefault()!;

                if (user != null)
                {
                    rm.SetResponse(true, "Consulta realizada exitosamente!.", "Obtener Usuario", user);
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo el usuario seleccionado!.", "Obtener Usuario");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}.", "Obtener Usuario");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Create(Usuario entity, Stream photo = null, string urlTemplateEmail = "")
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                #region check user
                var rmExists = await _context.Check(u => u.UsuaCedula == entity.UsuaCedula);
                if (rmExists.Response)
                {
                    rm.SetResponse(false, "Usuario existente!.", "Creación Usuario");
                    return rm;
                }
                #endregion

                string generateKey = _utilsService.GenerateKey();
                entity.UsuaPassword = _encryptionService.Hash256Text(generateKey);

                var rmCreate = await _context.Insert(entity);
                if (rmCreate.Response)
                {
                    Usuario userCreated = (Usuario)rmCreate.Result;

                    rm.SetResponse(true, "El usuario fue creado exitosamente!.", "Creación Usuario", entity);

                }
                else
                {
                    rm.SetResponse(false, "No se pudo crear el usuario!.", "Creación Usuario");
                }

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo crear el usuario: {ex.Message}.", "Creación Usuario");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Update(Usuario entity, Stream photo = null)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                #region check user
                var rmExists = await _context.Get(u => u.UsuaCedula == entity.UsuaCedula &&
                    u.UsuaCodigo != entity.UsuaCodigo);

                if (rmExists.Response)
                {
                    rm.SetResponse(false, "Usuario existente!.", "Actualización Usuario");
                    return rm;
                }
                #endregion

                #region reassign value user
                var rmQuery = await _context.Get(u => u.UsuaCodigo == entity.UsuaCodigo);
                IQueryable<Usuario> queryUser = rmQuery.Result;

                if (queryUser != null)
                {
                    Usuario userUpdate = queryUser.First();

                    userUpdate.UsuaNombre = entity.UsuaNombre;
                    userUpdate.UsuaCedula = entity.UsuaCedula;
                    userUpdate.UsuaEmail = entity.UsuaEmail;
                    userUpdate.UsuaTelefono = entity.UsuaTelefono;
                    userUpdate.UsuaPerfil = entity.UsuaPerfil;
                    userUpdate.UsuaFoto = entity.UsuaFoto;

                    var rmUpdate = await _context.Update(userUpdate);

                    if (rmUpdate.Response)
                    {
                        Usuario userProfileUpdated = userUpdate;
                        rm.SetResponse(true, "El usuario fue actualizado correctamente!.", "Actualización Usuario", userProfileUpdated);
                    }
                    else
                    {
                        rm.SetResponse(true, "No se pudo actualizar el usuario!.", "Actualización Usuario");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se obtuvo el usuario a actualizar!.", "Actualización Usuario");
                }
                #endregion
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo editar el usuario: {ex.Message}.", "Actualización Usuario");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> Delete(long id)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmUser = await _context.Get(u => u.UsuaCodigo == id);

                if (rmUser.Response)
                {
                    Usuario userToDelete = (Usuario)rmUser.Result;

                    string namePhoto = $"pic_profile_{userToDelete.UsuaCedula}";
                    userToDelete.UsuaEstado = 2;

                    var rmUserDelete = await _context.Update(userToDelete);

                    if (rmUserDelete.Response)
                    {
                        rm.SetResponse(true, "Usuario eliminado exitosamente!.", "Eliminar Usuario");
                    }
                    else
                    {
                        rm.SetResponse(false, "No se pudo eliminar el usuario!.", "Eliminar Usuario");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se pudo encontrar el usuario a eliminar!.", "Eliminar Usuario");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo eliminar el usuario: {ex.Message}.", "Eliminar Usuario");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> CheckLoginUser(string email, string password)
        {
            Utils.ResponseModel rm = new Utils. ResponseModel();

            try
            {
                string passwordEnc = _encryptionService.Hash256Text(password);
                var rmUserlogin = await _context.GetAll(u => u.UsuaEmail == email && u.UsuaPassword == passwordEnc);
                IQueryable<Usuario> query = (IQueryable<Usuario>)rmUserlogin.Result;

                if (query.Count() >= 1)
                {
                    var userLogin = query.FirstOrDefault();
                    rm.SetResponse(true, "Inicio de sesión exitoso!.", "Login", userLogin);
                }
                else
                {
                    rm.SetResponse(false, "Email o contraseña incorrecta!.", "Login");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"Ocurrió un error: {ex.Message}", "Login");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> ChangePassword(long id, string currentPassword, string newPassword)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmUser = await _context.Get(u => u.UsuaCodigo == id);

                string currentPasswordEnc = _encryptionService.Hash256Text(currentPassword);
                string newPasswordEnc = _encryptionService.Hash256Text(newPassword);

                if (rmUser.Response)
                {
                    Usuario userToUpdatePassword = (Usuario)rmUser.Result;

                    if (userToUpdatePassword.UsuaPassword != currentPasswordEnc)
                    {
                        rm.SetResponse(false, "La contraseña actual no coincide!.", "Actualización Contraseña");
                        return rm;
                    }

                    userToUpdatePassword.UsuaPassword = newPasswordEnc;

                    var rmUserUserUpdatePass = await _context.Update(userToUpdatePassword);

                    if (rmUserUserUpdatePass.Response)
                    {
                        rm.SetResponse(true, "Contraseña actualizada exitosamente!.", "Actualización Contraseña");
                    }
                    else
                    {
                        rm.SetResponse(false, "No se pudo eliminar el usuario!.", "Actualización Contraseña");
                    }
                }
                else
                {
                    rm.SetResponse(false, "No se pudo encontrar el usuario!.", "Actualización Contraseña");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo actualizar la contraseña del usuario: {ex.Message}.", "Actualización Contraseña");
            }

            return rm;
        }

        public async Task<Utils.ResponseModel> ResetPassword(long id, string destineEmail, string urlTemplateEmail)
        {
            Utils.ResponseModel rm = new Utils.ResponseModel();

            try
            {
                var rmUser = await _context.Get(u => u.UsuaCodigo == id);

                if (rmUser.Response)
                {
                    Usuario userFound = (Usuario)rmUser.Result;

                    string generateKey = _utilsService.GenerateKey();
                    userFound.UsuaPassword = _encryptionService.Hash256Text(generateKey);

                    #region send email with credentials
                    if (urlTemplateEmail != "")
                    {
                        urlTemplateEmail = urlTemplateEmail.Replace("[password]", generateKey);

                        string htmlEmail = "";
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlTemplateEmail);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            using (Stream dataStream = response.GetResponseStream())
                            {
                                StreamReader readerStream = null;

                                if (response.CharacterSet == null)
                                    readerStream = new StreamReader(dataStream);
                                else
                                    readerStream = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet));

                                htmlEmail = readerStream.ReadToEnd();
                                response.Close();
                                readerStream.Close();
                            }
                        }

                        if (htmlEmail != "")
                        {
                            Utils.ResponseModel rmIsSendEmail = await _emailService.SendEmail(destineEmail, "Contraseña restablecida!.", htmlEmail);

                            if (rmIsSendEmail.Response)
                            {
                                var rmUserResetPass = await _context.Update(userFound);

                                if (rmUserResetPass.Response)
                                {
                                    rm.SetResponse(true, "Contraseña restablecida exitosamente!.", "Restablecer Contraseña");
                                }
                                else
                                {
                                    rm.SetResponse(false, "No se pudo restablecer la contraseña!.", "Restablecer Contraseña");
                                }
                            }
                            else
                            {
                                rm.SetResponse(false, "No se pudo enviar el email con la contraseña restablecida, intentalo más tarde!.", "Restablecer Contraseña");
                            }
                        }
                    }
                    #endregion

                }
                else
                {
                    rm.SetResponse(false, "No se pudo encontrar el usuario!.", "Restablecer Contraseña");
                }
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo restablecer la contraseña del usuario: {ex.Message}.", "Restablecer Contraseña");
            }

            return rm;
        }
        #endregion
    }
}
