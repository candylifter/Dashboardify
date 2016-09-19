using System.Web.Http;
using System.Configuration;
using System.Net;
using System.Net.Http;
using Dashboardify.Contracts.Items;
using Dashboardify.Handlers.Items;

namespace Dashboardify.WebApi.Controllers
{
    public class ItemsController : ApiController
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DBIrmantas"].ConnectionString;

        [HttpGet]
        public HttpResponseMessage GetList(int dashboardId)
        {
            var request = new GetItemsListRequest();

            request.DashboarId = dashboardId;

            var handler = new GetItemsListHandler(connectionString);

            var response = handler.Handle(request);

            var httpStatusCode = response.HasErrors ? HttpStatusCode.BadRequest : HttpStatusCode.OK;

            return Request.CreateResponse(httpStatusCode, response);
        }

        [HttpPost]
        public HttpResponseMessage CreateItem(CreateItemRequest request)
        {
            var handler = new CreateItemHandler(connectionString);

            var response = handler.Handle(request);

            var httpStatusCode = response.HasErrors ? HttpStatusCode.BadRequest : HttpStatusCode.OK;

            return Request.CreateResponse(httpStatusCode, response);
        }

        [HttpPost]
        public HttpResponseMessage Update(UpdateItemRequest request)
        {
            var handler = new UpdateItemHandler(connectionString);

            var response = handler.Handle(request);

            var httpStatusCode = response.HasErrors ? HttpStatusCode.BadRequest : HttpStatusCode.OK;

            return Request.CreateResponse(httpStatusCode, response);
        }
    }
}
