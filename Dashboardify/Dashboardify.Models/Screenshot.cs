using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboardify.Models
{
    public class Screenshot
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ScrnshtURL { get; set; }
        public DateTime DateTaken { get; set; }

    }
}
