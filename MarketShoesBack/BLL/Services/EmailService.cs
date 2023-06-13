using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public class EmailService
    {

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmailConfirmationLink(string token, string email, string url)
        {
            string confirmationUrl = $"{url}/confirm/{token}";

            string subject = "Підтвердження пошти";
            string body = $"Для підтвердження пошти перейдіть за посиланням: {confirmationUrl}";

            SendEmail(email, subject, body);
        }

        public void SendPasswordResetEmail(string token, string email, string url)
        {
            string resetUrl = $"{url}/auth/reset/{token}";

            string subject = "Відновлення паролю";
            string body = $"Для відновлення паролю перейдіть за посиланням: {resetUrl}";

            SendEmail(email, subject, body);
        }

        private void SendEmail(string recipient, string subject, string body)
        {
            try
            {
                string emailUsername = _configuration["EmailSettings:Username"];
                string emailPassword = _configuration["EmailSettings:Password"];
                string emailHost = _configuration["EmailSettings:Host"];
                int emailPort = int.Parse(_configuration["EmailSettings:Port"]);

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(emailUsername),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(new MailAddress(recipient));

                SmtpClient smtpClient = new SmtpClient(emailHost, emailPort)
                {
                    Credentials = new NetworkCredential(emailUsername, emailPassword),
                    EnableSsl = true
                };
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка відправки електронної пошти: " + ex.Message);
            }
        }
    }
}
