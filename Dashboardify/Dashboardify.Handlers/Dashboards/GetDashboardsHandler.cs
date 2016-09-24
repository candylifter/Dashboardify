using System;
using Dashboardify.Repositories;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Dashboards;

namespace Dashboardify.Handlers.Dashboards
{
    public class GetDashboardsHandler : BaseHandler
    {
        private DashRepository _dashboardsRepository;

        private UserSessionRepository _userSessionRepository;

        public GetDashboardsHandler(string connectionString)
            : base(connectionString)
        {
            _dashboardsRepository = new DashRepository(connectionString);

            _userSessionRepository = new UserSessionRepository(connectionString);
        }

        public GetDashboardsResponse Handle(GetDashboardsRequest request)
        {
            var response = new GetDashboardsResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }

            try
            {
                var ticket = request.Ticket;

                var userId = _userSessionRepository.GetUserBySessionId(ticket).Id;

                var dashboards = _dashboardsRepository.GetByUserId(userId);

                response.Dashboards = dashboards;

                if (response.Dashboards == null)
                {
                    response.Errors.Add(new ErrorStatus("You dont have any dashboards")); //sita mes jeigu ir unauthorized
                    return response;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus("SYSTEM_ERROR"));

                // LOG TO FILE ex.Message

                return response;
            }
        }

        private IList<ErrorStatus> Validate(GetDashboardsRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (!IsSessionValid(request.Ticket))
            {
                errors.Add(new ErrorStatus("SESSION_NOT_VALID"));
                return errors;
            }
            
            return errors;
        }
    }
}
