using System.Linq;
using Dashboardify.Contracts.Items;
using Dashboardify.Handlers.Items;
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
            Assert.AreEqual(5,response.Errors.Count);
        }

        [Test]
        public void When0IdPassed_CreateItem()
        {
            var request = new CreateItemRequest();

            request.Item = new Item();

            request.Item.Id = 0;

            var handler = new CreateItemHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

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
            request.Item.CheckInterval = 30001;
            request.Item.XPath = "fdsgdfgfd";

            var handler = new CreateItemHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            var response = handler.Handle(request);

            Assert.AreEqual(2,response.Errors.Count);
            Assert.AreEqual("DASHBOARDID_NOT_DEFINED", response.Errors.First().Code);
        }

        [Test]
        public void DashboardNotExist_GetItemsList()
        {
            var request = new GetItemsListRequest();
            request.DashboarId = 0;
            var handler = new GetItemsListHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            var response = handler.Handle(request);

            Assert.True(response.HasErrors);
            Assert.AreEqual("DASHBOARDID_NOT_DEFINED",response.Errors.First().Code);
        }

        [Test]
        public void NegativeIdPassed_GetItemsList()
        {
            var request = new GetItemsListRequest();
            request.DashboarId = -5;
            var handler = new GetItemsListHandler("Data Source=DESKTOP-11VK3U9;Initial Catalog=DashBoardify;User id=DashboardifyUser;Password=123456;");

            var response = handler.Handle(request);

            Assert.True(response.HasErrors);
        }

        [Test]
        public void NullUser_UpdateUser()
        {
            //TODO search how to deal with exception in NUnit testing
        }
    }


}

