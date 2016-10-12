using System.Web.Http;
using System.Configuration;
using System.Net;
using System.Net.Http;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Dashboards;
using Dashboardify.Handlers.Users;
using Dashboardify.Models;
using Dashboardify.Security;

namespace Dashboardify.WebApi.Controllers
{

    public class UsersController : BaseController
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;
        
        [HttpPost]
        public HttpResponseMessage Update(User user, string ticket) //done, should i check if only id recieved?
        {
            var securityProvider = new SecurityProvider(_connectionString);

            var sessionInfo = securityProvider.GetSessionInfo(ticket);

            if (sessionInfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
            var updateRequest = new UpdateUserRequest
            {
                UserToUpdate = user,
                UserOrigin = sessionInfo.User,
            };

            var handler = new UpdateUserHandler(_connectionString);

            var response = handler.Handle(updateRequest);

            var statusCode = ResolveStatusCode(response);

            return Request.CreateResponse(statusCode, response);
        }

        [HttpGet]
        public HttpResponseMessage Create(string username, string password, string email, string invitationCode) //done
        {
            var handler = new CreateUserHandler(_connectionString);

            var response = handler.Handle(new CreateUserRequest
            {
                Email = email,
                Password = password,
                Username = username,
                InvitationCode = invitationCode
            });

            var createDashRequest = new CreateDashboardRequest
            {
                DashName = "Hello_DashBoardify",
                UserId = response.UserId
            };

            var createDashHandler = new CreateDashboardHandler(_connectionString);

            var createResponse = createDashHandler.Handle(createDashRequest);

            if (createResponse.HasErrors || response.HasErrors)
            {
                return Request.CreateResponse(HttpStatusCode.Accepted,response);
            }

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        [HttpGet] 
        public HttpResponseMessage Delete(string ticket)
        {
            var securityProvider = new SecurityProvider(_connectionString);

            var sessionInfo = securityProvider.GetSessionInfo(ticket);

            if (sessionInfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

            var deleteRequest = new DeleteUserRequest
            {
                Ticket = ticket,
                UserId = sessionInfo.User.Id
            };
            
            var handler = new DeleteUserHandler(_connectionString);

            var response = handler.Handle(deleteRequest);

            var httpStatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpStatusCode, response);
        }
    }
}
