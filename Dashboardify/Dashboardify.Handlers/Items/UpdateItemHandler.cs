using System;
using System.Collections.Generic;
using Dashboardify.Repositories;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Items;

namespace Dashboardify.Handlers.Items
{
    public class UpdateItemHandler
    {
        private ItemsRepository _itemRepository;

        public UpdateItemHandler(string connectionString)
        {
            _itemRepository = new ItemsRepository(connectionString);
        }

        public UpdateItemResponse Handle(UpdateItemRequest request)
        {
            var response = new UpdateItemResponse();

            if (response.HasErrors)
            {
                return response;
            }

            var item = _itemRepository.Get(request.Item.Id);

            if (item == null)
            {
                throw new Exception("ITEM_NOT_FOUND");
            }

            UpdateItemObject(item,request.Item);

            _itemRepository.Update(item);

            return response;
     

        }

        private void UpdateItemObject(Item origin, Item updated)
        {
            origin.LastChecked = updated.LastChecked;
            origin.CheckInterval = updated.CheckInterval;
            origin.Modified = updated.Modified;
            origin.XPath = updated.XPath;
            origin.Content = updated.Content;
        }

        private IList<ErrorStatus> Validate(UpdateItemRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (request.Item == null)
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }
            if (string.IsNullOrEmpty(request.Item.DashBoardId.ToString()))
            {
                errors.Add(new ErrorStatus("NO_DASHBOARDS_FOUND_ON_THIS_USER")); 
            }
            return errors;
        }
        
    }
}
