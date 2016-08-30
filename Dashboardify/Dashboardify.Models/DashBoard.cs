using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboardify.Models
{
    public class DashBoard
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public override string ToString()
        {
            return String.Format("ID : {0}, UserID: {1}, IsActive: {2}, Name: {3}, DateCreated : {4}, DateModified: {5}",Id,UserId,IsActive,Name,DateCreated,DateModified);
        }
    }
}
