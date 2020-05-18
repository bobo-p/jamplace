using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamPlace.IdentityServer4.Utils
{
    public interface ICustomEmailSender
    {
        void SendEmail(string email, string subject, string bodyMessage);
        void SendConfirmationEmail(string email, string callbackUrl);
        void SendResetPasswordEmail(string email, string callbackUrl);
        void SendDefaultPassword(string email, string password);
    }
}
