using System.Web.Http;
using System.Configuration;
using System.Net;
using System.Net.Http;
using Dashboardify.Contracts.Items;
using Dashboardify.Handlers.Items;

namespace Dashboardify.WebApi.Controllers
{
    public class ItemsController : BaseController
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;

        [HttpPost]
        public HttpResponseMessage GetList(GetItemsListRequest request)
        {
            var handler = new GetItemsListHandler(_connectionString);

            var response = handler.Handle(request);

            var httpStatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpStatusCode, response);
        }

        [HttpPost]
        public HttpResponseMessage CreateItem(CreateItemRequest request)
        {
            var handler = new CreateItemHandler(_connectionString);

            var response = handler.Handle(request);

            var httpStatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpStatusCode, response);
        }

        [HttpPost]
        public HttpResponseMessage Update(UpdateItemRequest request)
        {
            var handler = new UpdateItemHandler(_connectionString);

            var response = handler.Handle(request);

            var httpStatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpStatusCode, response);
        }

        [HttpPost]
        public HttpResponseMessage Delete(DeleteItemRequest request)
        {
            var handler = new DeleteItemHandler(_connectionString);

            var response = handler.Handle(request);

            var httpStatusCode = ResolveStatusCode(response);

            return Request.CreateResponse(httpStatusCode, response);
        }
    }
}
