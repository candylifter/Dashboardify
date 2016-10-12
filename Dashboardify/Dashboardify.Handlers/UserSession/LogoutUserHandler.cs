using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.UserSession;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.UserSession
{
    public class LogoutUserHandler:BaseHandler
    {
        private UserSessionRepository UserSessionRepository;

        public LogoutUserHandler(string connectionString):base (connectionString)
        {
            UserSessionRepository = new UserSessionRepository(connectionString);
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
                UserSessionRepository.DeleteUserSession(request.UserId);
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

            if (IsRequestNull(request))
            {
                errors.Add(new ErrorStatus("WRONG_REQUEST"));
                return errors;
            }

            
            return errors;
        }

    }
}
