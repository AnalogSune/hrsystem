using System.Threading.Tasks;
using API.Interfaces;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace API.Services
{
    public class MailService : IMailService
    {
        MailAddress _hrMail;
        public MailService()
        {
            _hrMail = new MailAddress("kon.ag.hr@gmail.com", "HR Support");
        }
        
        public async Task SendMessage(string subject, string body, string destination, string name)
        {
            var toAddress = new MailAddress(destination, name);
            const string fromPassword = "wQMuAWK4T7gE";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_hrMail.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(_hrMail, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                await smtp.SendMailAsync(message);
            }
        }
    }
}