using System;
using Dashboardify.Repositories;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Dashboards;

namespace Dashboardify.Handlers.Dashboards
{
    public class GetDashboardsHandler
    {
        private DashRepository _dashboardsRepository;

        private UserSessionRepository _userSessionRepository;

        public GetDashboardsHandler(string connectionString)
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
                var dashboards = _dashboardsRepository.GetByUserId(request.UserId);

                response.Dashboards = dashboards;

                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus(ex.Message));
                return response;
            }
            
        }

        private IList<ErrorStatus> Validate(GetDashboardsRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (request.UserId == 0)
            {
                errors.Add(new ErrorStatus("USERID_NOT_DEFINED"));
            }

            if (_userSessionRepository.GetExpireDate(request.SessionId) == DateTime.MinValue)
            {
                errors.Add(new ErrorStatus("SESSION_NULL_VALUE"));
                return errors;
            }

            if (_userSessionRepository.GetExpireDate(request.SessionId) < DateTime.Now)
            {
                errors.Add(new ErrorStatus("SESSION_EXPIRED"));
            }

            if (_userSessionRepository.GetExpireDate(request.SessionId) >= DateTime.MaxValue)
            {
                errors.Add(new ErrorStatus("WRONG_DATETIME_FORMAT"));
            }
            if (_userSessionRepository.GetUserBySessionId(request.SessionId).Id != request.UserId)
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACCES"));
            }


            return errors;
        }
    }
}
