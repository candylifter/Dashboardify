using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Handlers.Dashboards;

namespace Dashboardify.WebApi.Controllers
{
    public class DashboardsController : ApiController
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["DBZygis"].ConnectionString;
        //private DashRepository deshai = new DashRepository(ConnectionString);

        [HttpGet]
        public IHttpActionResult Index()
        {
            return Json(true);
        }

        [HttpGet]
        public HttpResponseMessage GetList(int userId)
        {
            var request = new GetDashboardsRequest();

            request.UserId = userId;

            var handler = new GetDashboardsHandler(ConnectionString);

            var response = handler.Handle(request);

            var httpStatusCode = response.HasErrors ? HttpStatusCode.BadRequest : HttpStatusCode.OK;

            return Request.CreateResponse(httpStatusCode, response);
        }

        //[HttpGet]
        //public IHttpActionResult GetDashboards()
        //{
        //    return Json(new { success = true, items = deshai.GetList() });
        //}
    }
}
