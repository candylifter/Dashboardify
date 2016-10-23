using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Dashboardify.Models;
using Dashboardify.Repositories;
using Dashboardify.Service.Classes;

namespace Dashboardify.Service.Helpers
{
    public static class EmailSenderHelper
    {
        public static void SendMessage(User user, Item item)
        {
            var fromAddress = new MailAddress("dashboardifyacademy@gmail.com", "Dashboardify");
            var toAddress = new MailAddress(user.Email, user.Name);
            string fromPassword = "desbordas";
            string subject = $"Dashboardify {item.Name} content changed";
            string img = @"https://pixabay.com/static/uploads/photo/2014/03/29/09/17/cat-300572_960_720.jpg";
            string body = $@"
            <p>Dear {user.Name},<br/><br/>
            
            The content of {item.Name} has changed.<br/><br/>
            For your convenience, we are attaching screenshot below, check it live <a href='{item.Website}'>here</a> or visit <a href='http://23.251.133.254/app'>Dashboardify</a> for more content tracking.<br/></p>
            <img src='http://23.251.133.254/screenshots/{item.Screenshots[0].ScrnshtURL}'/>";

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
                IsBodyHtml = true,
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
