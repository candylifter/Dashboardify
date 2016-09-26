using System.Linq;
using System.Net;
using System.Web.Http;
using Dashboardify.Contracts;

namespace Dashboardify.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        protected HttpStatusCode ResolveStatusCode(BaseResponse response)
        {
            var httpStatusCode = HttpStatusCode.OK;

            if (response.Errors.Any(e => e.Code == "SYSTEM_ERROR"))
            {
                return HttpStatusCode.InternalServerError;
            }

            if (response.Errors.Any(e => e.Code == "SESSION_NOT_VALID"))
            {
                return HttpStatusCode.Unauthorized;
            }

            if (response.Errors.Any(e => e.Code == "BAD_REQUEST"))
            {
                return HttpStatusCode.BadRequest;
            }
            if (response.Errors.Any(e => e.Code == "UNAUTHORIZED_ACCESS" || e.Code == "UNAUTHORIZED_ACCES" || e.Code == "UNAUTHORIZED_ACESS"))
            {
                return HttpStatusCode.Unauthorized;
            }
            //TODO prideti kitus kodus

            if (response.HasErrors)
            {
                return HttpStatusCode.BadRequest;
            }
            
            return httpStatusCode;
        }
    }
}
