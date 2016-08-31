using System;
using System.Collections.Generic;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers
{
    public class UpdateItemHandler
    {
        public bool Handle(Item item)
        {
            var status = true;

            var validationErrors = Validate(item);

            // If has validation errors
            if (validationErrors.Count != 0)
            {
                // Return errors   
            }

            Console.WriteLine("Greeting from handler!!! maestro");

            return status;
        }

        private IList<string> Validate(Item item)
        {
            var errors = new List<string>();

            if (item.Name == "Laba diena")
            {
                errors.Add("WRONG_NAME");
            }

            return errors;
        }
    }
}
