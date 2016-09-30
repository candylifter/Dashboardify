using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Dashboardify.Service.Helpers;

namespace Dashboardify.Service
{
    public static class MailSender
    {
       
        public static void SendMailContentChanged(List<UsernameItemEmailHelper> items)
        {
            foreach (var contact in items)
            {
                if (contact == null)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(contact.Email) || string.IsNullOrEmpty(contact.ItemName) ||
                    string.IsNullOrEmpty(contact.Username))
                {
                    continue;
                }

                SendMessage(contact);

            }
        }

        private static void SendMessage(UsernameItemEmailHelper contact)
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
