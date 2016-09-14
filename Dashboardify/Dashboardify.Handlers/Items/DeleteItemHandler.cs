using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Items;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Items
{
    public class DeleteItemHandler
    {
        private ItemsRepository _itemsRepository;

        public DeleteItemHandler(string connectionString)
        {
            _itemsRepository = new ItemsRepository(connectionString);
        }

        public DeleteItemResponse Handle(DeleteItemRequest request)
        {
            var response = new DeleteItemResponse();

            response.Errors = Validate(request);

            if(_itemsRepository.Get(request.Item.Id) == null)
            {
                throw new Exception("Item does not exsist");
            }


            if (response.HasErrors)
            {
                return response;
            }
            try
            {
                _itemsRepository.Delete(request.Item.Id);

                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus(ex.Message));

                return response;
            }
            

        }

        public IList<ErrorStatus> Validate(DeleteItemRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (request.Item.Id < 1)
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
            }
            
            return errors;
        }
    }
}
