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
        private static string connectionString = ConfigurationManager.ConnectionStrings["DBZygis"].ConnectionString;

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

        //public IHttpActionResult ToggleItem(int Id, int CheckInterval)
        //{
        //    var item = _itemsRepository.Get(Id);
        //    item.CheckInterval = CheckInterval;
        //    _itemsRepository.Update(item);
        //    return Json(true);
        //}
    }
}
