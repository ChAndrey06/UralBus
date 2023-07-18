using BLL.Entities.Mail;
using BLL.Interfaces;
using MailKit;
using MimeKit.Text;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace BLL.Logic
{
    public class MailRepository : IMailRepository
    {
        public async Task Send(Mail mail)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("from_address@example.com"));
            email.To.Add(MailboxAddress.Parse("to_address@example.com"));
            email.Subject = "Пришел запрос";
            email.Body = new TextPart(TextFormat.Html) { Text = @$"Имя: {mail.Name}, фамилия:{mail.Surname},  телефон {mail.Phone}" };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("[USERNAME]", "[PASSWORD]");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
