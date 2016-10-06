using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.WebApi.SecurityProvider
{
    public class DashSecurityProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;

        private UserSessionRepository _userSessionRepository;

        private DashRepository _dashRepository;

        private readonly BaseRequest _BaseRequest;

        private User _user;

        private UserSession _userSession;

        private UsersRepository _usersRepository;

        private BaseResponse _response;

        public DashSecurityProvider(BaseRequest baseRequest)
        {
            _userSessionRepository= new UserSessionRepository(_connectionString);
            _dashRepository = new DashRepository(_connectionString);
            _BaseRequest = baseRequest;
            _response = new BaseResponse();
            _response.Errors = new List<ErrorStatus>();
            _usersRepository = new UsersRepository(_connectionString);
        }
        
        public BaseResponse CheckForAnyBaseErrors()
        {
            if (_BaseRequest == null)
            {
                _response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return _response;
            }

            if (string.IsNullOrEmpty(_BaseRequest.Ticket))
            {
                _response.Errors.Add(new ErrorStatus("INVALID_TICKET"));
                return _response;
            }

            _user = _userSessionRepository.GetUserBySessionId(_BaseRequest.Ticket);

            _userSession = _userSessionRepository.GetSession(_BaseRequest.Ticket);

            if (_user == null || _userSession == null)
            {
                _response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return _response;
            }
            
            if (_userSessionRepository.GetSession(_BaseRequest.Ticket).Expires < DateTime.Now)
            {
                _response.Errors.Add(new ErrorStatus("SESSION_EXPIRED"));
                return _response;
            }

            return _response;
        }

        public UpdateDashboardResponse CheckForUpdateDashboardErrors(UpdateDashboardRequest request)
        {
            var updateDashResponse = new UpdateDashboardResponse();

            updateDashResponse.Errors = new List<ErrorStatus>();

            if (request == null)
            {
                updateDashResponse.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return updateDashResponse;

            }
            if (request.DashBoard == null)
            {
               updateDashResponse.Errors.Add(new ErrorStatus("BAD_REQUEST"));
               return updateDashResponse;
            }

            if (request.DashBoard.Id < 1)
            {
                updateDashResponse.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return updateDashResponse;
            }

            var requestUser = _userSessionRepository.GetUserBySessionId(request.Ticket);

            var ownerUser = _usersRepository.Get(request.DashBoard.UserId);

            if (requestUser == null || ownerUser == null)
            {
                updateDashResponse.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return updateDashResponse;
            }
            if (ownerUser.Id != requestUser.Id)
            {
                updateDashResponse.Errors.Add(new ErrorStatus("UNAUTHORIZED_ACCESS"));
                return updateDashResponse;
            }

            
            return updateDashResponse;
        }

        public CreateDashboardResponse CheckForCreateDashboardErrors(CreateDashboardRequest request)
        {
            var response = new CreateDashboardResponse();
            response.Errors = new List<ErrorStatus>();

            if (request == null)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return response;
            }

            if (string.IsNullOrEmpty(request.Ticket))
            {
                response.Errors.Add(new ErrorStatus("TICKET_NOT_DEFINED"));
                return response;
            }

            if (_userSessionRepository.GetSession(request.Ticket).Expires <DateTime.Now)
            {
                response.Errors.Add(new ErrorStatus("SESSION_TIME_OUT"));
                return response;
            }

            if (string.IsNullOrEmpty(request.DashName))
            {
                response.Errors.Add(new ErrorStatus("DASHBOARD_NOT_DEFINED"));
                return response;
            }
            var user = _userSessionRepository.GetUserBySessionId(request.Ticket);

            
            if (user == null)
            {
                response.Errors.Add(new ErrorStatus("USER_NOT_DEFINED"));
                return response;
            }

            if (!_dashRepository.CheckIfNameAvailable(user.Id, request.DashName))
            {
                response.Errors.Add(new ErrorStatus("NAME_ALREADY_EXISTS"));
            }

            return response;
        }

        public DeleteDashResponse CheckForDeleteDashboardErrors(DeleteDashRequest request)
        {
            var response = new DeleteDashResponse();
            response.Errors = new List<ErrorStatus>();

            if (request == null)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return response;

            }
            if (string.IsNullOrEmpty(request.DashboardId.ToString()))
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return response;
            }

            if (request.DashboardId < 1)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return response;
            }

            var requestUser = _userSessionRepository.GetUserBySessionId(request.Ticket);

            var userDashes = _dashRepository.GetByUserId(requestUser.Id);

            if (requestUser == null || userDashes == null)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return response;
            }

            if (!userDashes.Any(e => e.Id == request.DashboardId))
            
            {
                response.Errors.Add(new ErrorStatus("UNAUTHORIZED_ACCESS"));
                return response;
            }


            return response;
        }

        
        public User GetUser()
        {
            return _userSessionRepository.GetUserBySessionId(_BaseRequest.Ticket);
        }

        
    }
}
