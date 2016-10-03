using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Net.Http.Formatting;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Handlers.Dashboards;


namespace Dashboardify.WebApi.Controllers
{
    public class DashboardsController : BaseController
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;
        
        
        [HttpPost]
        public HttpResponseMessage GetList(GetDashboardsRequest request)
        {
            //var securityProvider = new SecurityProvider.SecurityProvider(request);

            //securityProvider.DoValidation();

            //var errors = securityProvider.ReturnErrors();

            //if (errors.Count > 0)
            //{
                
            //}

            var handler = new GetDashboardsHandler(_connectionString);

            var response = handler.Handle(request);

            var statusCode = ResolveStatusCode(response);

            return Request.CreateResponse(statusCode, response);
        }

        [HttpPost]
        public HttpResponseMessage Update(UpdateDashboardRequest request)
        {
            var handler = new UpdateDashBoardHandler(_connectionString);

            var response = handler.Handle(request);

            var statusCode = ResolveStatusCode(response);

            return Request.CreateResponse(statusCode, response);
        }

        [HttpPost]
        public HttpResponseMessage Create(CreateDashboardRequest request)
        {
            var handler = new CreateDashboardHandler(_connectionString);
            
            var response = handler.Handle(request);

            var statusCode = ResolveStatusCode(response);

            return Request.CreateResponse(statusCode, response);
        }

        [HttpPost]
        public HttpResponseMessage Delete(DeleteDashRequest request)
        {
            var handler = new DeleteDashHandler(_connectionString);

            var response = handler.Handle(request);

            var statusCode = ResolveStatusCode(response);

            return Request.CreateResponse(statusCode, response);
        }
    }
}
