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

            response.Errors = new List<ErrorStatus>();

            
                _dashRepository.Create(new DashBoard
                {
                    Name = request.DashName,
                    UserId = _userSessionRepository.GetUserBySessionId(request.Ticket).Id,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    IsActive = true
                });

                int userId = _userSessionRepository.GetUserBySessionId(request.Ticket).Id;

                var responseDash = _dashRepository.GetByNameAndUserId(request.DashName, userId);

                response.Dashboard = responseDash;

            
            
            return response;
        }

        
    }
}
