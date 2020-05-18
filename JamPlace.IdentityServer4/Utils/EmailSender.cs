using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JamPlace.IdentityServer4.Utils
{
    public class EmailSender : IEmailSender, ICustomEmailSender
    {
        public void SendConfirmationEmail(string email, string callbackUrl)
        {
            throw new NotImplementedException();
        }

        public void SendDefaultPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void SendEmail(string email, string subject, string bodyMessage)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
        public async void SendResetPasswordEmail(string email, string callbackUrl)
        {
            var fromAddress = new MailAddress("duperszpic1@gmail.com", "JamPlace");
            var toAddress = new MailAddress(email);
            const string fromPassword = "Frets123#";

            string pathToTemplate = GetPathToEmailTemplate("resetPassword");
            var builder = new StringBuilder();
            using (var reader = new StreamReader(pathToTemplate))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("{{callbaclUrl}}", callbackUrl);
            builder.Replace("{{email}}", email);

            using (var smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);
                using (var message = new MailMessage(fromAddress, toAddress))
                {
                    message.IsBodyHtml = true;
                    message.Subject = "Zresetuj hasło";
                    message.Body = builder.ToString();

                    smtp.SendCompleted += (sender, e) =>
                    {
                        if (e.Error != null)
                            Console.WriteLine(e.Error.ToString());
                        if (e.Cancelled)
                            Console.WriteLine("SendCompleted Cancelled");
                        message.Dispose();
                        smtp.Dispose();
                    };
                    await smtp.SendMailAsync(message);
                }
            }
        }
        private string GetPathToEmailTemplate(string templateName)
        {
            return Path.Combine(CustomPathHelper.GetRootPath(),
                "Emails", $"{templateName}.html");
        }
    }
}
