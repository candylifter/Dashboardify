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
        
        private UsersRepository _usersRepository;

        public DeleteDashHandler(string connectionString) : base(connectionString)
        {
            _dashRepository = new DashRepository(connectionString);
            
            _usersRepository = new UsersRepository(connectionString);
        }

        public DeleteDashResponse Handle(DeleteDashRequest request)
        {
            var response = new DeleteDashResponse {Errors = Validate(request)};


            if (response.HasErrors)
            {
                return response;
            }
            try
            {
                int userId = _usersRepository.Get(request.UserId).Id;

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
            
            var dash = _dashRepository.Get(request.DashboardId);

            if(dash == null)
            {
                errors.Add(new ErrorStatus("DASH_NOT_FOUND"));
                return errors;
            }
            
            if (request.UserId != dash.UserId)
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACCESS"));
                return errors;
            }
            

            return
            errors;
        }
    }
}
