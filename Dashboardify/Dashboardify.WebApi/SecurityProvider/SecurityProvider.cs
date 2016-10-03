using System;
using System.Collections.Generic;
using System.Configuration;
using Dashboardify.Contracts;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.WebApi.SecurityProvider
{
    public class SecurityProvider
    {
        private List<ErrorStatus> _errorStatuses;

        private BaseResponse _baseResponse;

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;

        private UserSessionRepository _userSessionRepository;

        private readonly BaseRequest _request;

        public SecurityProvider(BaseRequest request)
        {
            _userSessionRepository= new UserSessionRepository(_connectionString);
            _request = request;
            _errorStatuses = new List<ErrorStatus>();
        }

        public void DoValidation()
        {
            CheckIfNotExpired();
        }

        public void CheckIfNotExpired()
        {
            if (_userSessionRepository.GetSession(_request.Ticket).Expires < DateTime.Now)
            {
                _errorStatuses.Add(new ErrorStatus("SESSION_EXPIRED"));
            }
        }




        public List<ErrorStatus> ReturnErrors()
        {
            return _errorStatuses;
        }

        

    }
}
