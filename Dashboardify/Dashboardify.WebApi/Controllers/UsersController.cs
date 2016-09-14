using System.Web.Http;
using System.Configuration;
using System.Net;
using System.Net.Http;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Users;

namespace Dashboardify.WebApi.Controllers
{

    public class UsersController : ApiController
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DBZygis"].ConnectionString;
        
        [HttpPost]
        public HttpResponseMessage Update(UpdateUserRequest request)
        {
            var handler = new UpdateUserHandler(connectionString);

            var response = handler.Handle(request);

            var httpStatusCode = response.HasErrors ? HttpStatusCode.BadRequest : HttpStatusCode.OK;

            return Request.CreateResponse(httpStatusCode, response);
        }

        [HttpPost]
        public HttpResponseMessage Create(CreateUserRequest request)
        {
            var handler =new CreateUserHandler(connectionString);

            var responnse = handler.Handle(request);

            var httpSatusCode = responnse.HasErrors ? HttpStatusCode.BadRequest : HttpStatusCode.OK;

            return Request.CreateResponse(httpSatusCode, responnse);
        }
        
    }
}
