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

        private DashRepository _dashRepository;

        public UpdateItemHandler(string connectionString):base (connectionString)
        {
            _itemRepository = new ItemsRepository(connectionString);

            _userSessionRepository = new UserSessionRepository(connectionString);

            _dashRepository = new DashRepository(connectionString);
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
                var item = _itemRepository.Get(request.Item.Id);

                if (item == null)
                {
                    throw new Exception("ITEM_NOT_FOUND");
                }

                UpdateItemObject(item, request.Item);

                _itemRepository.Update(item);

                return response;
            }
            catch (Exception)
            {
                
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));

                return response;
            }

            
     

        }

        private void UpdateItemObject(Item origin, Item updated)
        {
            if (updated.LastChecked != DateTime.MinValue)
            {
                origin.LastChecked = updated.LastChecked;
            }
            if (!(origin.CheckInterval < 30000))
            {
                origin.LastChecked = updated.LastChecked;
            }
            if (updated.LastChecked != DateTime.MinValue)
            {
                origin.Modified = updated.Modified;
            }
            if (!string.IsNullOrEmpty(updated.XPath))
            {
                origin.XPath = updated.XPath;
            }
            if (!string.IsNullOrEmpty(updated.CSS))
            {
                origin.CSS = updated.CSS;
            }
            if (!string.IsNullOrEmpty(updated.CSS))
            {
                origin.Content = updated.Content;
            }
        }

        private IList<ErrorStatus> Validate(UpdateItemRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (IsRequestNull(request))
            {
                errors.Add(new ErrorStatus("WRONG_REQUEST"));
                return errors;
            }

            if (request.Item == null)
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }
            if (string.IsNullOrEmpty(request.Item.DashBoardId.ToString()))
            {
                errors.Add(new ErrorStatus("NO_DASHBOARDS_FOUND_ON_THIS_USER")); 
            }

            var user = _userSessionRepository.GetUserBySessionId(request.Ticket);

            if (user == null)
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
                return errors;
            }
            

            int ownerUserId = _dashRepository.Get(request.Item.DashBoardId).UserId;

            if (ownerUserId < 0)
            {
                errors.Add(new ErrorStatus("ITEM_NOT_FOUND"));
            }

            if (user.Id != ownerUserId)
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACCES"));
                return errors;
            }
            if (IsSessionValid(request.Ticket))
            {
                errors.Add(new ErrorStatus("SESSION_TIME_OUT"));
                return errors;
            }
            if (request.Item.Failed > 3)
            {
                errors.Add(new ErrorStatus("ITEM_"));//kuriam layeri ir kaip vyks update susirasti
            }



            return errors;
        }
        
    }
}
