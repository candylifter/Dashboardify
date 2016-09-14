using System.Net;
using System.Net.Mail;

namespace Dashboardify.Sandbox
{
    public class Email
    {
        public void Do()
        {
            TestSend();
        }

        private void TestSend()
        {
            var fromAddress = new MailAddress("dashboardifyacademy@gmail.com", "Dashboardify");
            var toAddress = new MailAddress("zygimantas.zilevicius@gmail.com", "Zygimantas");
            const string fromPassword = "desbordas";
            const string subject = "Hello Email World";
            const string body = "Parduodu opeli";

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
