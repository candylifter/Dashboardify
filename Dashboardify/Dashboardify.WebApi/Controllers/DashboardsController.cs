using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dashboardify.Repositories;
using System.Configuration;

namespace Dashboardify.WebApi.Controllers
{

    public class DashboardsController : ApiController
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["DBIrmantas"].ConnectionString;
        private DashRepository deshai = new DashRepository(ConnectionString);

        [HttpGet]
        public IHttpActionResult Index()
        {
            return Json(true);
        }
        [HttpGet]
        public IHttpActionResult GetDashboards()
        {
            return Json(new { success = true, items = deshai.GetList() });
        }
    }
}
