using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dashboardify.Contracts.Items;
using Dashboardify.Handlers.Items;
using Dashboardify.Repositories;
using Dashboardify.Security;

namespace Dashboardify.WebApi.Controllers
{
    public class ItemsController : BaseController
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;

        [HttpGet]
        public HttpResponseMessage GetList(int dashboardId, string ticket)
        {

            // Call security provider to check if session is valid

            var securityProvider = new SecurityProvider(_connectionString);

            var sessionInfo = securityProvider.GetSessionInfo(ticket);

            if (sessionInfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

            var getListRequest = new GetItemsListRequest
            {
                DashboardId = dashboardId,
                User = sessionInfo.User
            };
        
            var handler = new GetItemsListHandler(_connectionString);

            var response = handler.Handle(getListRequest);

            var httpStatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpStatusCode, response);
            
        }

        [HttpPost]
        public HttpResponseMessage CreateItem(string ticket, int dashId, Item item)
        {
            var securityProvider = new SecurityProvider(_connectionString);

            var sessionInfo = securityProvider.GetSessionInfo(ticket);

            if (sessionInfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

            var createRequest = new CreateItemRequest
            {
                Item = item,
                DashId = dashId,
                UserId = sessionInfo.User.Id

            };

            var handler = new CreateItemHandler(_connectionString);

            var response = handler.Handle(createRequest);

            var httpStatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpStatusCode, response);
        }

        [HttpPost]
        public HttpResponseMessage Update(string ticket, Item item)
        {
            var securityProvider = new SecurityProvider(_connectionString);

            var sessionInfo = securityProvider.GetSessionInfo(ticket);

            if (sessionInfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

            var updateRequest = new UpdateItemRequest
            {
                Item = item,
                UserId = sessionInfo.User.Id
            };
           
            var handler = new UpdateItemHandler(_connectionString);

            var response = handler.Handle(updateRequest);

            var httpStatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpStatusCode, response);
        }

        [HttpGet]
        public HttpResponseMessage Delete(string ticket, int itemId)
        {
            var securityProvider = new SecurityProvider(_connectionString);

            var sessionInfo = securityProvider.GetSessionInfo(ticket);

            if (sessionInfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

            var deleteRequest = new DeleteItemRequest
            {
                ItemId = itemId,
                UserId = sessionInfo.User.Id,
            };

            var handler = new DeleteItemHandler(_connectionString);

            var response = handler.Handle(deleteRequest);

            var httpStatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpStatusCode, response);
        }
    }
}
