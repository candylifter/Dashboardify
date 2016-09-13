using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Items;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Items
{
    public class GetItemsListHandler
    {
        private ItemsRepository _itemsRepository;
        private ScreenshotRepository _screenshotRepository;

        public GetItemsListHandler(string connectionString)
        {
            _itemsRepository = new ItemsRepository(connectionString);
            _screenshotRepository = new ScreenshotRepository(connectionString);
        }

        public GetItemsListResponse Handle(GetItemsListRequest request)
        {
            var response = new GetItemsListResponse();

            response.Errors = Validate(request);
                        
            if (response.HasErrors)
            {
                return response;
            }

            var items = _itemsRepository.GetByDashboardId(request.DashboarId);

            var itemsWithScreenshots = new List<Item>();

            foreach (var item in items)
            {
                var screenshot = _screenshotRepository.GetLastByItemId(item.Id);

                if (screenshot != null)
                {
                    item.Screenshots.Add(screenshot);
                }

                itemsWithScreenshots.Add(item);
            }

            response.Items = itemsWithScreenshots;

            return response;
        }

        private IList<ErrorStatus> Validate(GetItemsListRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (request.DashboarId < 1)
            {
                errors.Add(new ErrorStatus("DASHBOARDID_NOT_DEFINED"));
            }

            return errors;
        }
    }
}
