using System.Web.Http;
using System.Configuration;
using System.Net;
using System.Net.Http;
using Dashboardify.Contracts.UserSession;
using Dashboardify.Handlers.UserSession;

namespace Dashboardify.WebApi.Controllers
{
    public class LoginController : BaseController
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;

        [HttpPost]
        public HttpResponseMessage Index(LoginUserRequest request)
        {
            var handler = new LoginUserHandler(_connectionString);

            var response = handler.Handle(request);

            var httpSatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpSatusCode, response);
        }

        [HttpPost]
        public HttpResponseMessage LogOut(LogoutUserRequest request)
        {
            var handler = new LogoutUserHandler(_connectionString);

            var response = handler.Handle(request);

            var httpStatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpStatusCode);


        }
    }
}