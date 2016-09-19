using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboardify.Contracts;
using Dashboardify.Contracts.UserSession;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.UserSession
{
    public class LogoutUserHandler
    {
        public UserSessionRepository _userSessionRepository;

        public LogoutUserHandler(string connectionString)
        {
            _userSessionRepository = new UserSessionRepository(connectionString);
        }


        public LogoutUserResponse Handle(LogoutUserRequest request)
        {
            var response = new LogoutUserResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }
            try
            {
                _userSessionRepository.DeleteUserSession(request.SessionId);
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus(ex.Message));
            }

            return response;
        }

        private IList<ErrorStatus> Validate(LogoutUserRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (string.IsNullOrEmpty(request.SessionId))
            {
                errors.Add(new ErrorStatus("NO_SESSION_ID_RECIEVED"));
                return errors;
            }

            return errors;
        }

    }
}
