using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application.Service
{
    public class SendEmail
    {
        public void send(string UserName, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("EmailName"));
            email.To.Add(MailboxAddress.Parse(UserName));
            email.Subject = "Confirm your account";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            //Here i'm using smtp.ethereal to test, you can also use another SMTP server provides.  
            smtp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("EmailName", "Password");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
