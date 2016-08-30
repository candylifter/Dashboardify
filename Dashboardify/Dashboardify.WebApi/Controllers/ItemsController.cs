using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dashboardify.WebApi.Controllers
{
    public class ItemsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Json(true);
        }

    }
}
