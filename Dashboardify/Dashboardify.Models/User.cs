using System;

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

        public override string ToString()
        {
            return
                String.Format(
                    "Id : {0} , Name: {1}, Password: {2}, Email: {3}, IsActive: {4}, DateRegistered: {5}, DateModified: {6}",
                    Id, Name, Password, Email, IsActive, DateRegistered, DateModified);
        }
    }

    


}

