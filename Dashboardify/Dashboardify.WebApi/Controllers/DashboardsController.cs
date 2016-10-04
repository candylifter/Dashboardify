using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Handlers.Dashboards;
using Dashboardify.WebApi.SecurityProvider;


namespace Dashboardify.WebApi.Controllers
{
    public class DashboardsController : BaseController
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["GCP"].ConnectionString;
        
        
        [HttpPost]
        public HttpResponseMessage GetList(BaseRequest request)
        {

            var response = new BaseResponse();

            var securityProvider = new DashSecurityProvider(request);
            
            response = securityProvider.CheckForAnyBaseErrors();

            if (response.HasErrors)
            {
                var statusCodeFail = ResolveStatusCode(response);       //nezinau ar geriausias sprendimas, bet PX, veikia, net peles neatitrauki

                return Request.CreateResponse(statusCodeFail,response);
            }
            
            var user = securityProvider.GetUser();

            var GetDashBoardsRequest = new GetDashboardsRequest{ Id = user.Id};

            var handler = new GetDashboardsHandler(_connectionString);

            response = handler.Handle(GetDashBoardsRequest);

            var statusCode = ResolveStatusCode(response);

            return Request.CreateResponse(statusCode, response);
            

            
        }

        [HttpPost]
        public HttpResponseMessage Update(UpdateDashboardRequest request)
        {
            var response = new BaseResponse();

            var securityProvider = new DashSecurityProvider(request);

            response = securityProvider.CheckForAnyBaseErrors();

            var authentificationResponse = new UpdateDashboardResponse();

            authentificationResponse = securityProvider.CheckForAuthentificationErrors(request);

            if (response.HasErrors)
            {
                var statusCodeFail = ResolveStatusCode(response);

                return Request.CreateResponse(statusCodeFail, response);
            }
            if (authentificationResponse.HasErrors)
            {
                var statusCodeFail = ResolveStatusCode(authentificationResponse);

                return Request.CreateResponse(statusCodeFail, authentificationResponse);
            }
            
            var updateDashBoardRequest = new UpdateDashboardRequest
            {
                DashBoard = request.DashBoard
            };

            var handler = new UpdateDashBoardHandler(_connectionString);

            response = handler.Handle(updateDashBoardRequest);

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
