using System;
using Dashboardify.Repositories;

namespace Dashboardify.Security
{
    public class SecurityProvider
    {
        private UserSessionRepository _userSessionRepository;

        public SecurityProvider(string connectionString)
        {
            _userSessionRepository = new UserSessionRepository(connectionString);
            
        }

        public SessionInfo GetSessionInfo(string ticket)
        {
            var session = _userSessionRepository.GetSession(ticket);
            
            if (session == null || session.Expires < DateTime.Now)
            {
                return null;
            }
            

            var user = _userSessionRepository.GetUserBySessionId(ticket);


            var sessionInfo = new SessionInfo
            {
                User = user,
                Session = session
            };

            

            return sessionInfo;
        }

        

    }
}
