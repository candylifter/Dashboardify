using Dashboardify.Repositories;
using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Items;

namespace Dashboardify.Handlers.Items
{
    class GetItemHandler:BaseHandler
    {
        private ItemsRepository _itemRepository;

        private UserSessionRepository _userSessionRepository;


        public GetItemHandler(string connectionString):base(connectionString)
        {
            _itemRepository = new ItemsRepository(connectionString);

            _userSessionRepository = new UserSessionRepository(connectionString);
        }

        public GetItemResponse Handle(GetItemRequest request)
        {
            var response = new GetItemResponse();

            if (response.HasErrors)
            {
                return response;
            }
            
            _itemRepository.Get(request.ItemId);

            return response;


        }

        private IList<ErrorStatus> Validate(GetItemRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (IsRequestNull(request))
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }

            if (string.IsNullOrEmpty(request.Ticket))
            {
                errors.Add(new ErrorStatus("INVALID_TICKET"));
                return errors;
            }

            if (request.ItemId == null || request.ItemId <= 0)
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }

            var user = _userSessionRepository.GetUserBySessionId(request.Ticket);

            var userOwner = _itemRepository.GetUserByItemId(request.ItemId);

            if (user == null || userOwner == null)
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
                return errors;
            }

            if (user.Id != userOwner.Id)
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACESS"));
            }

            return errors;
        }
    }
}
