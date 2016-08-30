using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dashboardify.Repositories;
using System.Web.Script.Serialization;
using System.Configuration;

namespace Dashboardify.WebApi.Controllers
{
    public class ItemsController : ApiController
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["DBIrmantas"].ConnectionString;
        private ItemsRepository aitemai = new ItemsRepository(ConnectionString);

        [HttpGet]
        public IHttpActionResult Index()
        {
            return Json(true);
        }

        [HttpGet]
        public IHttpActionResult GetItems()
        {
            return Json(new { success = true, items = aitemai.GetList() });
        }

        public IHttpActionResult ToggleItem(int Id, int CheckInterval)
        {
            var item = aitemai.Get(Id);
            item.CheckInterval = CheckInterval;
            aitemai.Update(item);
            return Json(true);
        }
    }
}
