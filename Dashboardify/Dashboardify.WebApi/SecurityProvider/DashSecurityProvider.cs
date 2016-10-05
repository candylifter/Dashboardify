using System;
using System.Collections.Generic;
using System.Configuration;
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

        private readonly BaseRequest _request;

        private User _user;

        private UserSession _userSession;

        private UsersRepository _usersRepository;

        private BaseResponse _response;

        public DashSecurityProvider(BaseRequest request)
        {
            _userSessionRepository= new UserSessionRepository(_connectionString);
            _dashRepository=new DashRepository(_connectionString);
            _request = request;
            _response = new BaseResponse();
            _response.Errors = new List<ErrorStatus>();
            _usersRepository = new UsersRepository(_connectionString);
        }
        
        public BaseResponse CheckForAnyBaseErrors()
        {
            if (_request == null)
            {
                _response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return _response;
            }

            if (string.IsNullOrEmpty(_request.Ticket))
            {
                _response.Errors.Add(new ErrorStatus("INVALID_TICKET"));
                return _response;
            }

            _user = _userSessionRepository.GetUserBySessionId(_request.Ticket);

            _userSession = _userSessionRepository.GetSession(_request.Ticket);

            if (_user == null || _userSession == null)
            {
                _response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return _response;
            }
            
            if (_userSessionRepository.GetSession(_request.Ticket).Expires < DateTime.Now)
            {
                _response.Errors.Add(new ErrorStatus("SESSION_EXPIRED"));
                return _response;
            }

            return _response;
        }

        public UpdateDashboardResponse CheckForAuthentificationErrors(UpdateDashboardRequest request)
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

        
        public User GetUser()
        {
            return _userSessionRepository.GetUserBySessionId(_request.Ticket);
        }

        
    }
}
