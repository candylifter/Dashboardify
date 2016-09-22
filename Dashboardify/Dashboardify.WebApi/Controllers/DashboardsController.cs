using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Handlers.Dashboards;

namespace Dashboardify.WebApi.Controllers
{
    public class DashboardsController : BaseController
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;
        //private DashRepository deshai = new DashRepository(ConnectionString);

        [HttpGet]
        public IHttpActionResult Index()
        {
            return Json(true);
        }

        [HttpPost]
        public HttpResponseMessage GetList(GetDashboardsRequest request)
        {
            var handler = new GetDashboardsHandler(_connectionString);

            var response = handler.Handle(request);

            var statusCode = ResolveStatusCode(response);

            return Request.CreateResponse(statusCode, response);
        }
    }
}
