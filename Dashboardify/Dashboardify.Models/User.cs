using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dashboardify.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Password
        {
            get { return this.Password; }
            set { this.Password = HashPassword(Password); }
        }
        

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public DateTime Registered { get; set; }

        public DateTime Modified { get; set; }

        private string HashPassword(string pass)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(pass);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }


    }

    


}

