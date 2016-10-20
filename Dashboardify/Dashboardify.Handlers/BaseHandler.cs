using System;

namespace Dashboardify.Handlers
{
    public class BaseHandler
    {
        protected string ConnectionString;

        public BaseHandler(string connectionString)
        {
            if (connectionString == null) throw new ArgumentException("connectionString");

            ConnectionString = connectionString;
          
        }

    }
}
