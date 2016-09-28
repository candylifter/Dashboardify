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
            _userSessionRepository = new UserSessionRepository(connectionString);
      
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

            if (IsRequestNull(request))
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }
            if (request.Item == null || request.Item.Id < 1)
            {
                errors.Add(new ErrorStatus("ITEM_NOT_DEFINED"));
            }

            var RequestUser = _userSessionRepository.GetUserBySessionId(request.Ticket);

            var OwnerUser = _itemsRepository.GetUserByItemId(request.Item.Id);

            if (RequestUser == null || OwnerUser == null)
            {
                errors.Add(new ErrorStatus("ITEM_NOT_FOUND"));
                return errors;
            }

    if (

        RequestUser.Id != OwnerUser.Id)
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACESS"));
                return errors;
            }
            if (!IsSessionValid(request.Ticket))
            {
                errors.Add(new ErrorStatus("SESSION_EXPIRED"));
            }

            
            if (request.Item.Id < 1)
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
            }
            
            return errors;
        }
    }
}
