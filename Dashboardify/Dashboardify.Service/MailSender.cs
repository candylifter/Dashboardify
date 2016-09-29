using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboardify.Repositories;

namespace Dashboardify.Service
{
    public static class MailSender
    {
        

        public static void SendMailContentChanged(List<Item> items)
        {
            foreach (var item in items)
            {
                if (item.NotifyByEmail)
                {
                    
                }    
            }
        }
    }
}
