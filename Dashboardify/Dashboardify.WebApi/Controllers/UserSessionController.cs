using System.Web.Http;
using System.Configuration;
using System.Net;
using System.Net.Http;
using Dashboardify.Contracts.Users;
using Dashboardify.Contracts.UserSession;
using Dashboardify.Handlers.Users;
using Dashboardify.Handlers.UserSession;

namespace Dashboardify.WebApi.Controllers
{
    public class UserSessionController : ApiController
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;


        //TODO Fails, neprisiskiria Name n Passowrd User modelyje
        [HttpPost]
        public HttpResponseMessage Login(LoginUserRequest request)
        {
            var handler = new LoginUserHandler(connectionString);

            var response = handler.Handle(request);

            var httpSatusCode = response.HasErrors ? HttpStatusCode.BadRequest : HttpStatusCode.OK;

            return Request.CreateResponse(httpSatusCode, response);
        }
    }
}