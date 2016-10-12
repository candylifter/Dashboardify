using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Items;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Items
{
    public class GetItemsListHandler : BaseHandler
    {
        private ItemsRepository _itemsRepository;
        private ScreenshotRepository _screenshotRepository;
        private DashRepository _dashRepository;

        public GetItemsListHandler(string connectionString) :
            base(connectionString)
        {
            _itemsRepository = new ItemsRepository(connectionString);
            _screenshotRepository = new ScreenshotRepository(connectionString);
            
            _dashRepository = new DashRepository(connectionString);
        }

        public GetItemsListResponse Handle(GetItemsListRequest request) //TODO need to check for null 
        {
            var response = new GetItemsListResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }
            try
            {
                var items = _itemsRepository.GetByDashboardId(request.DashboardId);

                var itemsWithScreenshots = new List<Item>();

                foreach (var item in items)
                {
                    var screenshots = _screenshotRepository.GetLastsByItemId(item.Id, 15);

                    if (screenshots.Count > 0)
                    {
                        item.Screenshots = screenshots;
                    }

                    itemsWithScreenshots.Add(item);
                }

                response.Items = itemsWithScreenshots;

                return response;
            }
            catch (Exception)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return response;
            }
        }

        private IList<ErrorStatus> Validate(GetItemsListRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (IsRequestNull(request))
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }


            var requestUser = request.User;

            var ownerUser = _dashRepository.GetUserByDashId(request.DashboardId);

            if (requestUser == null || ownerUser == null )
            {
                errors.Add(new ErrorStatus("WRONG_REQUEST"));
                return errors;
            }
        
            
            if (requestUser.Id != ownerUser.Id)
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACESS"));
                return errors;
            }



            return errors;
        }
    }
}
