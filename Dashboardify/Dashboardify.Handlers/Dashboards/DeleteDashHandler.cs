using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Dashboards
{
    public class DeleteDashHandler : BaseHandler
    {
        private DashRepository _dashRepository;

        private UserSessionRepository _userSessionRepository;

        public DeleteDashHandler(string connectionString) : base(connectionString)
        {
            _dashRepository = new DashRepository(connectionString);

            _userSessionRepository = new UserSessionRepository(connectionString);
        }

        public DeleteDashResponse Handle(DeleteDashRequest request)
        {
            var response = new DeleteDashResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }
            try
            {
                int userId = _userSessionRepository.GetUserBySessionId(request.Ticket).Id;

                _dashRepository.DeleteDashboard(userId, request.DashboardId);

            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
            }

            return response;

        }

        private IList<ErrorStatus> Validate(DeleteDashRequest request)
        {
            var errors = new List<ErrorStatus>();
            
            var user = _userSessionRepository.GetUserBySessionId(request.Ticket);

            if (user == null)
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
                return errors;
            }

          
            if (_dashRepository.CheckIfExistsByUserId(request.DashboardId, user.Id))
            {
                errors.Add(new ErrorStatus("DASHBOARD_DOES_NOT_EXIST"));
                return errors;
            }

            return
            errors;
        }
    }
}
