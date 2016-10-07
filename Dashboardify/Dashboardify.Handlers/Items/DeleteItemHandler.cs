using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Items;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Items
{
    public class DeleteItemHandler : BaseHandler
    {
        private ItemsRepository _itemsRepository;
        private UserSessionRepository _userSessionRepository;


        public DeleteItemHandler(string connectionString):base (connectionString)
        {
            _itemsRepository = new ItemsRepository(connectionString);
            
        }

        public DeleteItemResponse Handle(DeleteItemRequest request)
        {
            var response = new DeleteItemResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }
            try
            {
                _itemsRepository.Delete(request.ItemId);

                return response;
            }
            catch (Exception)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));

                return response;
            }
            

        }

        public IList<ErrorStatus> Validate(DeleteItemRequest request)
        {
            var errors = new List<ErrorStatus>();

            var ItemOwner = _itemsRepository.GetUserByItemId(request.ItemId);

            if (ItemOwner == null)
            {
                errors.Add(new ErrorStatus("ITEM_NOT_FOUND"));
                return errors;
            }

            if (string.IsNullOrEmpty(ItemOwner.Id.ToString()))
            {
                errors.Add(new ErrorStatus("ITEM_NOT_FOUND")); // kaip cia validatinti
                return errors;
            }

          
            if (request.UserId != ItemOwner.Id)
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACCESS"));
                return errors;
            }
            if (request.ItemId < 1)
            {
                errors.Add(new ErrorStatus("WRONG_ID"));
            }
            
            return errors;
        }
    }
}
