using Dashboardify.Repositories;
using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Items;

namespace Dashboardify.Handlers.Items
{
    class GetItemHandler
    {
        private ItemsRepository _itemRepository;

        public GetItemHandler(string connectionString)
        {
            _itemRepository = new ItemsRepository(connectionString);
        }

        public GetItemResponse Handle(GetItemRequest request)
        {
            var response = new GetItemResponse();

            if (response.HasErrors)
            {
                return response;
            }

            var item = _itemRepository.Get(request.ItemId);

            if (item == null)
            {
                throw new Exception("ITEM_NOT_FOUND");
            }

            _itemRepository.Get(request.ItemId);

            return response;


        }

        private IList<ErrorStatus> Validate(GetItemRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (request.ItemId == null || request.ItemId == 0)
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }

            return errors;
        }
    }
}
