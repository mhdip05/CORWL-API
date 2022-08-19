using NMS_API_N.IServices;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace NMS_API_N.Services
{
    public class MailServices : IEmailServices
    {
        private readonly IConfiguration _config;

        public MailServices(IConfiguration config)
        {
            _config = config;
        }
        public bool SendMail(string toMail, string subject, string body)
        {
            EmailConfig(toMail, subject, body);
            return true;
        }

        private async void EmailConfig(string toMail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetValue<string>("MailSettings:FromMail")));
            email.To.Add(MailboxAddress.Parse(toMail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            await GetSmtp(email);
        }

        private async Task GetSmtp(MimeMessage email)
        {
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;
            await smtp.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.Auto);
            await smtp.AuthenticateAsync(_config.GetValue<string>("MailSettings:FromMail"), _config.GetValue<string>("MailSettings:MailSecret"));
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

        }
    }
}
