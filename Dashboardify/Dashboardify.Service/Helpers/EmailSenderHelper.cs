using System.Net;
using System.Net.Mail;
using Dashboardify.Service.Classes;

namespace Dashboardify.Service.Helpers
{
    public static class EmailSenderHelper
    {
        public static void SendMessage(UsernameEmailItem contact)
        {
            var fromAddress = new MailAddress("dashboardifyacademy@gmail.com", "Dashboardify");
            var toAddress = new MailAddress(contact.Email, contact.Username);
            const string fromPassword = "desbordas";
            const string subject = "Dashboardify item content changed";
            string body = "Dear " + contact.Username + $"\n The content of your selected item {contact.ItemName} has changed";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
