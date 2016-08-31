using System.Web.Http;
using System.Configuration;
using Dashboardify.Contracts.Items;
using Dashboardify.Handlers.Items;

namespace Dashboardify.WebApi.Controllers
{
    public class ItemsController : ApiController
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DBZygis"].ConnectionString;

        [HttpGet]
        public IHttpActionResult GetList(int dashboardId)
        {
            var request = new GetItemsListRequest();

            request.DashboarId = dashboardId;

            var handler = new GetItemsListHandler(connectionString);

            var response = handler.Handle(request);

            return Json(response);
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
