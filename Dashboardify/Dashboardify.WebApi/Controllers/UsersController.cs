using System.Web.Http;
using System.Configuration;
using System.Net;
using System.Net.Http;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Users;

namespace Dashboardify.WebApi.Controllers
{

    public class UsersController : BaseController
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;
        
        [HttpPost]
        public HttpResponseMessage Update(UpdateUserRequest request)
        {
            var handler = new UpdateUserHandler(_connectionString);

            var response = handler.Handle(request);

            var statusCode = ResolveStatusCode(response);

            return Request.CreateResponse(statusCode, response);
        }

        [HttpPost]
        public HttpResponseMessage Create(CreateUserRequest request)
        {
            var handler = new CreateUserHandler(_connectionString);

            var response = handler.Handle(request);

            var httpSatusCode = response.HasErrors ? HttpStatusCode.BadRequest : HttpStatusCode.OK;

            return Request.CreateResponse(httpSatusCode, response);
        }
    }
}
