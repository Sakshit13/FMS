using Castle.Core.Smtp;
using System.Net.Mail;
using System.Net;

namespace FMS._23.V1.Services
{
    public class SEmailSender : IEmailSender
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public SEmailSender(string smtpServer = "webmail.sparxitsolutions.com", int smtpPort = 465, string smtpUsername = "shivanand.pathak@sparxitsolutions.com", string smtpPassword = "Shivanand@1234")
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var smtpClient = new SmtpClient(_smtpServer)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpUsername),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                throw ex;
            }
        }

        void IEmailSender.Send(string from, string to, string subject, string messageText)
        {
            throw new NotImplementedException();
        }

        void IEmailSender.Send(MailMessage message)
        {
            throw new NotImplementedException();
        }

        void IEmailSender.Send(IEnumerable<MailMessage> messages)
        {
            throw new NotImplementedException();
        }
    }

}
