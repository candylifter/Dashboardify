using System.Configuration;
using Dashboardify.Contracts;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.WebApi.SecurityProvider
{
    public class SecurityProvider
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;

        private UserSessionRepository _userSessionRepository;

        private readonly BaseRequest _request;

        public SecurityProvider(BaseRequest request)
        {
            _userSessionRepository= new UserSessionRepository(_connectionString);
            _request = request;
        }

        public CheckSessionValidation()
        {
            if (_userSessionRepository.GetUserBySessionId(_request.))
        }

    }
}
