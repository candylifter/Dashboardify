using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dashboardify.Repositories;
using System.Web.Script.Serialization;

namespace Dashboardify.WebApi.Controllers
{
    public class ItemsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Json(true);
        }
        [HttpGet]
        public IHttpActionResult GetItems()
        {
            ItemsRepository aitemai = new ItemsRepository("Data Source=DESKTOP-ECC2U9D\\SQLEXPRESS;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");
            return Json(new { success = true ,items = aitemai.GetList() });
        }
    }
}
