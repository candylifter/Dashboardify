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
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
         
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateRegistered { get; set; }

        public DateTime DateModified { get; set; }

        //private string HashPassword(string pass)
        //{
        //    MD5 md5 = MD5.Create();
        //    byte[] inputBytes = Encoding.ASCII.GetBytes(pass);
        //    byte[] hash = md5.ComputeHash(inputBytes);

        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < hash.Length; i++)
        //    {
        //        sb.Append(hash[i].ToString("X2"));
        //    }
        //    return sb.ToString();
        //}

        public override string ToString()
        {
            return
                String.Format(
                    "Id : {0} , Name: {1}, Password: {2}, Email: {3}, IsActive: {4}, DateRegistered: {5}, DateModified: {6}",
                    Id, Name, Password, Email, IsActive, DateRegistered, DateModified);
        }
    }

    


}

