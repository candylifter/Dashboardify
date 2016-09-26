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

        protected bool IsSessionValid(string ticket)
        {
            var session = _userSessionRepository.GetSession(ticket); //parasyti po to metoda

            return session != null && session.Expires > DateTime.Now; 
        }

        protected bool IsRequestNull(object request)
        {
            if (request == null)
            {
                return true;
            }
            return false;
        }
    }
}
