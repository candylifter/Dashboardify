using System.Web.Http;
using System.Configuration;
using System.Net;
using System.Net.Http;
using Dashboardify.Contracts.UserSession;
using Dashboardify.Handlers.UserSession;
using Dashboardify.Security;

namespace Dashboardify.WebApi.Controllers
{
    public class LoginController : BaseController
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;

        [HttpPost] //done
        public HttpResponseMessage Index(LoginUserRequest request)
        {
            var handler = new LoginUserHandler(_connectionString);

            var response = handler.Handle(request);

            var httpSatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpSatusCode, response);
        }

        [HttpGet]
        public HttpResponseMessage LogOut(string ticket)
        {
            var securityProvider = new SecurityProvider(_connectionString);

            var sessionInfo = securityProvider.GetSessionInfo(ticket);

            if (sessionInfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

            var logOutRequest = new LogoutUserRequest
            {
                UserId = sessionInfo.User.Id
            };

            var handler = new LogoutUserHandler(_connectionString);

            var response = handler.Handle(logOutRequest);

            var httpStatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpStatusCode);


        }
    }
}