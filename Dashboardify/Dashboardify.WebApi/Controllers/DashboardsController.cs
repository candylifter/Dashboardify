using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Net;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Handlers.Dashboards;
using Dashboardify.Models;
using Dashboardify.Security;


namespace Dashboardify.WebApi.Controllers
{
    public class DashboardsController : BaseController
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;
        
        [HttpGet]
        public HttpResponseMessage GetList(string ticket)
        {
            

            var securityProvider = new SecurityProvider(_connectionString);

            var sessionInfo = securityProvider.GetSessionInfo(ticket);

            if (sessionInfo == null)
            {
                
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            var getListRequest = new GetDashboardsRequest
            {
                Id = sessionInfo.User.Id
            };

            var handler = new GetDashboardsHandler(_connectionString);

            var response = handler.Handle(getListRequest);

            var statusCode = ResolveStatusCode(response);

            return Request.CreateResponse(statusCode, response);

        }

        [HttpPost]
        public HttpResponseMessage Update(string ticket, DashBoard dash)
        {
            var securityProvider = new SecurityProvider(_connectionString);

            var sessionInfo = securityProvider.GetSessionInfo(ticket);

            if (sessionInfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            var updateRequest = new UpdateDashboardRequest
            {
                DashBoard = dash, //TODO FIX HERE
                UserId = sessionInfo.User.Id
            };

            var handler = new UpdateDashBoardHandler(_connectionString);

            var response = handler.Handle(updateRequest);

            var statusCode = ResolveStatusCode(response);

            return Request.CreateResponse(statusCode, response);
        }

        [HttpGet]
        public HttpResponseMessage Create(string ticket, string dashName)
        {
            var securityProvider = new SecurityProvider(_connectionString);

            var sessionInfo = securityProvider.GetSessionInfo(ticket);

            if (sessionInfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            var deleteRequest = new CreateDashboardRequest
            {
                DashName = dashName,
                UserId = sessionInfo.User.Id
            };

            var handler = new CreateDashboardHandler(_connectionString);

            var response = handler.Handle(deleteRequest);

            var statusCode = ResolveStatusCode(response);

            return Request.CreateResponse(statusCode, response);

        }

        [HttpGet]
        public HttpResponseMessage Delete(string ticket, int id)
        {
            var securityProvider = new SecurityProvider(_connectionString);

            var sessionInfo = securityProvider.GetSessionInfo(ticket);

            if (sessionInfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            var deleteRequest = new DeleteDashRequest
            {
                DashboardId = id,
                UserId = sessionInfo.User.Id //fix
            };

            var handler = new DeleteDashHandler(_connectionString);

            var response = handler.Handle(deleteRequest);

            var statusCode = ResolveStatusCode(response);

            return Request.CreateResponse(statusCode, response);

        }
    }
}
