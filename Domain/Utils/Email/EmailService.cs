
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;
using Domain.Utils.Utils;
using Infrastructure.Repository;

namespace Domain.Utils.Email
{
    public class EmailService : IEmailService
    {
        #region properties
        private readonly IGenericRepository<ConfiguracionGeneral> _configuracion;
        #endregion

        #region constructor
        public EmailService(IGenericRepository<ConfiguracionGeneral> configuracion)
        {
            _configuracion = configuracion;
        }
        #endregion

        public async Task<ResponseModel> SendEmail(string destineMail, string subject, string message)
        {
            ResponseModel rm = new ResponseModel();

            try
            {
                var rmQuery = await _configuracion.GetAll(c => c.CogeService.Equals("MAIL_SERVICE"));
                var query = (IQueryable<ConfiguracionGeneral>)rmQuery.Result;

                Dictionary<string, string> config = query.ToDictionary(keySelector: c => c.CogeKey, elementSelector: c => c.CogeValue);

                var credentials = new NetworkCredential(config["email"], config["password"]);

                var emailSend = new MailMessage()
                {
                    From = new MailAddress(config["email"], config["alias"]),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                emailSend.To.Add(new MailAddress(destineMail));

                var serverClient = new SmtpClient()
                {
                    Host = config["host"],
                    Port = int.Parse(config["port"]),
                    Credentials = credentials,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true
                };

                serverClient.Send(emailSend);
                rm.SetResponse(true, "Correo electrónico enviado exitosamente!.", "Envío de Email");
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, $"No se pudo enviar el email: {ex.Message}");
            }

            return rm;
        }
    }
}
