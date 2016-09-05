using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Items;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Items
{
    public class GetItemsListHandler
    {
        private ItemsRepository _itemsRepository;

        public GetItemsListHandler(string connectionString)
        {
            _itemsRepository = new ItemsRepository(connectionString);
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

            response.Items = items;

            return response;
        }

        private IList<ErrorStatus> Validate(GetItemsListRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (request.DashboarId == 0)
            {
                errors.Add(new ErrorStatus("DASHBOARDID_NOT_DEFINED"));
            }

            return errors;
        }
    }
}
