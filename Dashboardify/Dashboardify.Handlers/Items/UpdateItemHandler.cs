using System;
using System.Collections.Generic;
using Dashboardify.Repositories;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Items;

namespace Dashboardify.Handlers.Items
{
    public class UpdateItemHandler :BaseHandler
    {
        private ItemsRepository _itemRepository;

        private UserSessionRepository _userSessionRepository;

        public UpdateItemHandler(string connectionString):base (connectionString)
        {
            _itemRepository = new ItemsRepository(connectionString);

            _userSessionRepository = new UserSessionRepository(connectionString);

        }

        public UpdateItemResponse Handle(UpdateItemRequest request)
        {
            var response = new UpdateItemResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }
            try
            {
                var item = _itemRepository.Get(request.ItemId);

                if (item == null)
                {
                    throw new Exception("ITEM_NOT_FOUND");
                }

                UpdateItemObject(item,request.Name,request.CheckInterval,request.IsActive,request.NotifyByEmail);

                _itemRepository.Update(item);

                return response;
            }
            catch (Exception)
            {
                
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));

                return response;
            }

            
        }

        private void UpdateItemObject(Item origin, string requestName, int requestInterval, bool requestIsActive, bool requestNotifyByEmail)
        {
            
            if (!(requestInterval < 30000 && requestInterval > 86400000))
            {
                origin.CheckInterval = requestInterval;
            }

            if (!string.IsNullOrEmpty(requestName))
            {
                origin.Name = requestName;
            }

            if (requestIsActive)
            {
                origin.IsActive = requestIsActive;
            }
            else
            {
                origin.IsActive = false;
            }

            if (requestNotifyByEmail)
            {
                origin.NotifyByEmail = requestNotifyByEmail;
            }
            else
            {
                origin.NotifyByEmail = false;
            }
            
            origin.Modified = DateTime.Now;
          
        }

        private IList<ErrorStatus> Validate(UpdateItemRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (IsRequestNull(request))
            {
                errors.Add(new ErrorStatus("WRONG_REQUEST"));
                return errors;
            }

            if (request.ItemId < 1)
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                errors.Add(new ErrorStatus("NO_DASHBOARDS_FOUND_ON_THIS_USER")); 
            }
            if (string.IsNullOrEmpty(request.Ticket))
            {
                errors.Add(new ErrorStatus("INVALID_TICKET"));
                return errors;
            }
            if (request.CheckInterval < 30000 && request.CheckInterval > 86400000)
            {
                errors.Add(new ErrorStatus("INVALID_CHECK_INTERVAL"));
                return errors;
            }
            
            var user = _userSessionRepository.GetUserBySessionId(request.Ticket);

            var ownerUser = _itemRepository.GetUserByItemId(request.ItemId);

            if (user == null)
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
                return errors;
            }
            
            if (ownerUser == null)
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }

            if (ownerUser.Id < 1)
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }

            if (user.Id != ownerUser.Id)
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACCES"));
                return errors;
            }
            if (!IsSessionValid(request.Ticket))
            {
                errors.Add(new ErrorStatus("SESSION_TIME_OUT"));
                return errors;
            }
            
            return errors;
        }
        
    }
}
