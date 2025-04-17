using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils.Email
{
    public interface IEmailService
    {
        #region methods
        Task<ResponseModel> SendEmail(string destineMail, string subject, string message);
        #endregion
    }
}
