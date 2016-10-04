using System;
using System.Collections.Generic;
using Dashboardify.Repositories;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Dashboards;

namespace Dashboardify.Handlers.Dashboards
{
    public class GetDashboardsHandler : BaseHandler
    {
        private DashRepository _dashboardsRepository;
        
        public GetDashboardsHandler(string connectionString)
            : base(connectionString)
        {
            _dashboardsRepository = new DashRepository(connectionString);
        }

        public GetDashboardsResponse Handle(GetDashboardsRequest request)
        {
            var response = new GetDashboardsResponse();

            response.Errors = new List<ErrorStatus>();
            
            try
            {
                var dashboards = _dashboardsRepository.GetByUserId(request.Id);

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

        
    }
}
