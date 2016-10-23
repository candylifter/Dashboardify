using System;
using System.Collections.Generic;
using Dashboardify.Repositories;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Items;

namespace Dashboardify.Handlers.Items
{
    public class CreateItemHandler:BaseHandler
    {
        private ItemsRepository _itemRepository;

        private DashRepository _dashRepository;

        public CreateItemHandler(string connectionString): 
            base (connectionString)
        {
            _itemRepository = new ItemsRepository(ConnectionString);

            _dashRepository = new DashRepository(ConnectionString);
        }

        public CreateItemResponse Handle(CreateItemRequest request)  // TODO NEEDS WORK
        {
            var response = new CreateItemResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }

            try
            {
                var now = DateTime.Now;

                request.Item.IsActive = true;
                request.Item.UserNotified = false;
                request.Item.NotifyByEmail = true;
                request.Item.Created = now;
                request.Item.Modified = now;
                request.Item.LastChecked = now.AddMinutes(-60);
                request.Item.Content = "";

                _itemRepository.Create(request.Item);

                response.Success = true;

                return response;
            }
            catch (Exception)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return response;
            }

        }

        private IList<ErrorStatus> Validate(CreateItemRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (request.Item.DashBoardId == 0)
            {
                errors.Add(new ErrorStatus("DASHBOARDID_NOT_DEFINED"));
            }

            if (request.Item.CheckInterval < 30000 && request.Item.CheckInterval> 86400000) 
            {
                errors.Add(new ErrorStatus("CHECKINTERVAL_WRONG"));
            }

            if (string.IsNullOrEmpty(request.Item.XPath))
            {
                errors.Add(new ErrorStatus("XPATH_NOT_DEFINED"));
            }

            if (string.IsNullOrEmpty(request.Item.CSS))
            {
                errors.Add(new ErrorStatus("CSS_NOT_DEFINED"));
            }

            if (string.IsNullOrEmpty(request.Item.Website))
            {
                errors.Add(new ErrorStatus("WEBSITE_NOT_DEFINED"));
            }

            if (string.IsNullOrEmpty(request.Item.Name))
            {
                errors.Add(new ErrorStatus("NAME_NOT_DEFINED"));
            }

            var userIdByDash = _dashRepository.GetUserByDashId(request.Item.DashBoardId);
            var requestUserId = request.UserId;

            if (userIdByDash != null && requestUserId != userIdByDash.Id) //TODO pasiklausti zilvino ar good practice
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACCESS"));
            }

            

            return errors;
        }
    }
}
