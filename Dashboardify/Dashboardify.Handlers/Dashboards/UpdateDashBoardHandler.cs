using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Dashboards
{
    public class UpdateDashBoardHandler:BaseHandler
    {
        private DashRepository _dashRepository;

        private UserSessionRepository _userSessionRepository;

        public UpdateDashBoardHandler(string connectionString) :base(connectionString)
        {
            _dashRepository = new DashRepository(connectionString);
        }

        public UpdateDashboardResponse Handle(UpdateDashboardRequest request)
        {
            var response = new UpdateDashboardResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }
            try
            {
                var dashOrigin = _dashRepository.Get(request.DashBoard.Id);

                UpdateDashObject(dashOrigin, request.DashBoard);

                _dashRepository.Update(dashOrigin);

                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return response;
            }
            
        }

        private void UpdateDashObject(DashBoard origin, DashBoard request)
        {
            if (!string.IsNullOrEmpty(request.Name))
            {
                origin.Name = request.Name;
            }

            origin.DateModified = DateTime.Now;
            
        }

        public IList<ErrorStatus> Validate(UpdateDashboardRequest request)
        {
            var errors = new List<ErrorStatus>();
           
            if (_dashRepository.Get(request.DashBoard.Id)==null)
            {
                errors.Add(new ErrorStatus("DASH_NOT_FOUND"));
                return errors;
            }
            
            if (request.DashBoard.Id < 1)
            {
                errors.Add(new ErrorStatus("CORRUPTED_ID"));
                return errors;
            }
            return errors;
        }
    }
}
