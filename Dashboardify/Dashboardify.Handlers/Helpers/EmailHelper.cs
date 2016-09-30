using System.Net;
using System.Net.Mail;
using Dashboardify.Contracts.Users;

namespace Dashboardify.Handlers.Helpers
{
    public class EmailHelper
    {
        public static void SendEmail(CreateUserRequest request)//TODO refactor to mailsender in service layer
        {
            var fromAddress = new MailAddress("dashboardifyacademy@gmail.com", "Dashboardify");
            var toAddress = new MailAddress(request.Email, request.Username);
            const string fromPassword = "desbordas";
            const string subject = "Welcome";
            string body = "Dear " + request.Username + "\n We are happy that you are using our dashboardify app. (ITERPTI MAESTRO TRUMPA)";

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

        public static void SendNewPasswordEmail(string email, string password, string username)
        {
            var fromAddress = new MailAddress("dashboardifyacademy@gmail.com", "Dashboardify");
            var toAddress = new MailAddress(email, username);
            const string fromPassword = "desbordas";
            const string subject = "Welcome";
            string body = $@"Dear " + username + "\n We are happy that you are using our dashboardify app. (ITERPTI MAESTRO TRUMPA) YOUR NEW PASSWORD IS " + password;

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
