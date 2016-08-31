using System.Web.Http;
using System.Configuration;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Users;

namespace Dashboardify.WebApi.Controllers
{

    public class UsersController : ApiController
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DBZygis"].ConnectionString;
        
        [HttpPost]
        public IHttpActionResult Update(UpdateUserRequest request)
        {
            var handler = new UpdateUserHandler(connectionString);

            var response = handler.Handle(request);

            return Json(response);
        }
    }
}
