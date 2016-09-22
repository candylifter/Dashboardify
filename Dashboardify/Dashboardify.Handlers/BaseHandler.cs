using System;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers
{
    public class BaseHandler
    {
        protected string ConnectionString;

        private UserSessionRepository _userSessionRepository;

        public BaseHandler(string connectionString)
        {
            if(connectionString == null) throw new ArgumentException("connectionString");

            ConnectionString = connectionString;

            _userSessionRepository = new UserSessionRepository(ConnectionString);
        }

        protected bool CheckIfSessionValid(string sessionId)
        {   //TODO sudeti logika
            return false;
        }
    }
}
