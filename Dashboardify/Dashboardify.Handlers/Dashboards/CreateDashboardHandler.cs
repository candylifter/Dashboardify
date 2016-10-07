using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Dashboards
{
    public class CreateDashboardHandler:BaseHandler
    {
        private DashRepository _dashRepository;

        private UserSessionRepository _userSessionRepository;

        public CreateDashboardHandler(string connectionString) : base(connectionString)
        {
            _dashRepository = new DashRepository(connectionString);

            _userSessionRepository = new UserSessionRepository(connectionString);
        }

        public CreateDashboardResponse Handle(CreateDashboardRequest request)
        {
            var response = new CreateDashboardResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }
            try
            {
                
                _dashRepository.Create(new DashBoard
                {
                    Name = request.DashName,
                    UserId = request.UserId,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    IsActive = true
                });

                int userId = _userSessionRepository.GetUserBySessionId(request.DashName).Id;

                var responseDash = _dashRepository.GetByNameAndUserId(request.DashName, userId);

                response.Dashboard = responseDash;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
            }
            
            return response;
        }

        public List<ErrorStatus> Validate(CreateDashboardRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (string.IsNullOrEmpty(request.DashName))
            {
                errors.Add(new ErrorStatus("DASH_NAME_NOT_DEFINED"));
                return errors;
            }
            if (string.IsNullOrEmpty(request.UserId.ToString()))
            {
                errors.Add(new ErrorStatus("BAD_ID"));
                return errors;
            }
          

            return errors;
        }

        
    }
}
