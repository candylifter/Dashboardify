using System;
using System.Linq;
using Dashboardify.Contracts.Items;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Items;
using Dashboardify.Models;
using Dashboardify.Repositories;
using NUnit.Framework;

namespace Dashboardify.Handlers.Test
{
    [TestFixture]
    public class ItemHandlerTest
    {

        [Test]
        public void WhenNullObjectPassed_CreateItem()
        {
            // Set up
            var request = new CreateItemRequest();

            request.Item = new Item();

            var handler =
                new CreateItemHandler(
                    "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            // Act
            var response = handler.Handle(request);

            // Assert
            Assert.True(response.HasErrors);
            Assert.AreEqual(5, response.Errors.Count);
        }

        [Test]
        public void When0IdPassed_CreateItem()
        {
            var request = new CreateItemRequest();

            request.Item = new Item();

            request.Item.Id = 0;

            var handler =
                new CreateItemHandler(
                    "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            var response = handler.Handle(request);

            Assert.True(response.HasErrors);
            Assert.AreEqual("DASHBOARDID_NOT_DEFINED", response.Errors.First().Code);

        }

        [Test]
        public void WhenWrongIntervalPassed_CreateItem()
        {
            var request = new CreateItemRequest();

            request.Item = new Item();

            request.Item.DashBoardId = 1;
            request.Item.CheckInterval = 10001;
            request.Item.XPath = "fdsgdfgfd";

            var handler =
                new CreateItemHandler(
                    "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            var response = handler.Handle(request);

            Assert.AreEqual(3, response.Errors.Count);
            Assert.AreEqual("NAME_NOT_DEFINED",response.Errors[2].Code);
            Assert.AreEqual("WEBSITE_NOT_DEFINED", response.Errors[1].Code);
            Assert.AreEqual("CHECKINTERVAL_WRONG",response.Errors[0].Code);
        }

        [Test]
        public void DashboardNotExist_GetItemsList()
        {
            var request = new GetItemsListRequest();
            request.DashboarId = 0;
            var handler =
                new GetItemsListHandler(
                    "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            var response = handler.Handle(request);

            Assert.True(response.HasErrors);
            Assert.AreEqual("DASHBOARDID_NOT_DEFINED", response.Errors.First().Code);
        }

        [Test]
        public void NegativeIdPassed_GetItemsList()
        {
            var request = new GetItemsListRequest();
            request.DashboarId = -5;
            var handler =
                new GetItemsListHandler(
                    "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            var response = handler.Handle(request);

            Assert.True(response.HasErrors);
        }

        [Test]
        public void NullItem_UpdateItem()
        {
            var request = new UpdateItemRequest();
            request.Item = new Item();
            request.Item = null;

            var handler =
                new UpdateItemHandler(
                    "Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            var response = handler.Handle(request);

            Assert.True(response.HasErrors);

            


        }

        [Test]
        public void CorruptedId_DeleteItem()
        {
            var request = new DeleteItemRequest();
            request.Item = new Item()
            {
              Id  = 5456

            };
            
            var handler = new DeleteItemHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            //var response = handler.Handle(request);

            //Assert.True(response.HasErrors);


            var ex = Assert.Throws<Exception>(() => handler.Handle(request));
            Assert.That(ex.Message, Is.EqualTo("Item does not exsist"));

        }

        [Test]
        public void CheckIfWorks_DeleteItem()
        {
            var request = new DeleteItemRequest();
            request.Item = new Item()
            {
                Id = 5
            };

            var handler = new DeleteItemHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");
            Assert.True(!handler.Handle(request).HasErrors);
        }



    }


}

