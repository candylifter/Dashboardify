using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Contracts.Items
{
    public class UpdateItemRequest:BaseRequest
    {
        public Item item { get; set; }
    }
}
