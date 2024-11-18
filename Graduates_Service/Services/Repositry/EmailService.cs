using Graduates_Model.Model;
using Graduates_Service.Services.Repositry.IRepositry;
using MailKit.Net.Smtp;
using MimeKit;

namespace Graduates_Service.Services.Repositry
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfigration _emailConfigration;

        public EmailService (EmailConfigration emailConfigration)
        {
            _emailConfigration = emailConfigration;
        }

        public void SendEmail(Messsage message)
        {
            var emailMesssage = CreateEmailMessage(message);
            Send(emailMesssage);
        }

        private MimeMessage CreateEmailMessage(Messsage message)
        {
            var EmailMessage = new MimeMessage();
            EmailMessage.From.Add(new MailboxAddress("email",_emailConfigration.From));
            EmailMessage.To.AddRange(message.To);
            EmailMessage.Subject = message.Subject;
            EmailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            // Add recipients to Cc
            //EmailMessage.Cc.Add(new MailboxAddress("CC Recipient", "hamza.hot2002@gmail.com"));
            string email = message.To[0].ToString();
            // Add recipients to Bcc
            EmailMessage.Bcc.Add(new MailboxAddress("BCC Recipient", $"{email}"));

            return EmailMessage;
        }

        private void Send(MimeMessage MailMesssage)
        {
            using var client = new SmtpClient();
            try
            {
                // Connect to SMTP server with appropriate security options
                client.Connect(_emailConfigration.SmtpServer, _emailConfigration.Port, true);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Authenticate with credentials
                client.Authenticate(_emailConfigration.UserName, _emailConfigration.Password);

                // Send the email
                client.Send(MailMesssage);

            }
            catch 
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
