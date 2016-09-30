using System;
using System.Collections.Generic;
using System.Linq;
using Dashboardify.Service.Classes;

namespace Dashboardify.Sandbox
{
    public class Service
    {

        private List<UsernameEmailItem> list;

        public void Do()
        {
            
            list = new List<UsernameEmailItem>();

            var user = new UsernameEmailItem();

            user.Email = "";
            user.ItemName = "Audi";
            user.Username = "Zilas";

            list.Add(user);
            
            TestEmailSender(list);

        }

        public void TestEmailSender(List<UsernameEmailItem> listas)
        {
            Console.WriteLine("Sending email to: " + listas.First().Email);

            //MailSender.SendMailContentChanged(listas);
        }
    }
}
