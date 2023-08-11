using MailKit.Security;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;

namespace EmailService.Services
{
    public class MailService : IMailService
    {
        private readonly SmtpSettings _smtpSettings;

        public MailService(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress($"{_smtpSettings.SmtpSender}", _smtpSettings.SmtpUsername));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.SmtpServer, _smtpSettings.SmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpSettings.SmtpUsername, _smtpSettings.SmtpPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }

}
