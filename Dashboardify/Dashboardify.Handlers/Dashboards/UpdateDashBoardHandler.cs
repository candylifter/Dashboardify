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
            _userSessionRepository = new UserSessionRepository(connectionString);
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
                response.Errors.Add(new ErrorStatus(ex.Message));
                return response;
            }
            
        }

        private void UpdateDashObject(DashBoard origin, DashBoard request)
        {
            origin.DateModified = DateTime.Now;
            origin.Name = request.Name;
        }

        public IList<ErrorStatus> Validate(UpdateDashboardRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (IsRequestNull(request))
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }

            if (_dashRepository.Get(request.DashBoard.Id)==null)
            {
                errors.Add(new ErrorStatus("DASH_NOT_FOUND"));
                return errors;
            }

            if (string.IsNullOrEmpty(request.Ticket))
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
            }
            if (request.DashBoard.Id < 1)
            {
                errors.Add(new ErrorStatus("CORRUPTED_ID"));
            }

            var requestUser = _userSessionRepository.GetUserBySessionId(request.Ticket);

            var ownerUser = _dashRepository.GetUserByDashId(request.DashBoard.Id);

            if (IsRequestNull(requestUser) || IsRequestNull(ownerUser))
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
                return errors;
            }

            if (requestUser.Id != ownerUser.Id)
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACCES"));
            }
            
            return errors;
        }
    }
}
