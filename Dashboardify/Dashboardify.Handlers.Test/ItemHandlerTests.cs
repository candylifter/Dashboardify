using System;
using System.Collections.Generic;
using Dashboardify.Contracts.Items;
using Dashboardify.Handlers.Dashboards;
using Dashboardify.Handlers.Items;
using Dashboardify.Models;
using Dashboardify.Repositories;
using Moq;
using NUnit.Framework;

namespace Dashboardify.Handlers.Test
{
    [TestFixture]
    public class ItemHandlerTest
    {

        [Test]
        public void Test_Create_Item()
        {

            List<DashBoard> listas = new List<DashBoard>();


            listas.Add(new DashBoard
            {
                Id = 5
            });

            var request = new CreateItemRequest
            {
                DashId = 5,
                Item =
                {
                    CheckInterval = 5000,
                    Content = "string",
                    Created = DateTime.Now,
                    CSS = "string",
                    DashBoardId = 5,
                    Failed = 0,
                    Id = 5,
                    IsActive = true,
                    LastChecked = DateTime.Now.AddDays(-1),
                    Modified = DateTime.Now
                }
            };

            var handler = new CreateItemHandler("SSSS");

            handler.Handle(request);

            //act



        }
    

    }


}

