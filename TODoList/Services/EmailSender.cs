using MailKit.Net.Smtp;
using MimeKit;

namespace TODoList.Services
{
    public class EmailSender
    {
        private readonly string SmtpServer;
        private readonly int SmtpPort;
        private readonly string SmtpUsername;
        private readonly string SmtpPassword;

        public EmailSender(IConfiguration configuration)
        {
            SmtpServer = configuration.GetValue<string>("SmtpSettings:SmtpServer", "");
            SmtpPort = configuration.GetValue<int>("SmtpSettings:SmtpPort", 0);
            SmtpUsername = configuration.GetValue<string>("SmtpSettings:SmtpUsername", "");
            SmtpPassword = configuration.GetValue<string>("SmtpSettings:SmtpPassword", "");
        }

        public void SendEmail(string senderName, string senderEmail, string toName, string toEmail, string subject , string textContent)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.To.Add(new MailboxAddress(toName, toEmail));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = textContent
            };
            using (var client = new SmtpClient())
            {
                client.Connect(SmtpServer, SmtpPort, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(SmtpUsername, SmtpPassword);

                try
                {
                    var result = client.Send(message);
                    Console.WriteLine("Email Sender Ok: \n" + result);
                    client.Disconnect(true);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Email Sender Failure: \n" + e.ToString());

                }
            }
        }


    }
}
