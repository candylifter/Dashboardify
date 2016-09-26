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

        private UserSessionRepository _userSessionRepository;

        private DashRepository _dashRepository;

        public CreateItemHandler(string connectionString): 
            base (connectionString)
        {
            _itemRepository = new ItemsRepository(ConnectionString);

            _userSessionRepository = new UserSessionRepository(ConnectionString);

            _dashRepository = new DashRepository(ConnectionString);
        }

        public CreateItemResponse Handle(CreateItemRequest request) 
        {
            var response = new CreateItemResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }

            try
            {
                request.Item.IsActive = true;
                request.Item.Created = DateTime.Now;
                request.Item.Modified = DateTime.Now;
                request.Item.LastChecked = DateTime.Now;
                request.Item.Content = "";

                _itemRepository.Create(request.Item);

                response.Success = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus(ex.Message));
                return response;
            }

        }

        private IList<ErrorStatus> Validate(CreateItemRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (IsRequestNull(request))
            {
                errors.Add(new ErrorStatus("WRONG_REQUEST"));
                return errors;
            }

            if (IsRequestNull(request.Item))
            {
                errors.Add(new ErrorStatus("ITEM_NOT_DEFINED"));
                return errors;
            }

            if (request.Item.DashBoardId == 0)
            {
                errors.Add(new ErrorStatus("DASHBOARDID_NOT_DEFINED"));
            }

            if (request.Item.CheckInterval > 30000) //neigiamas irgi negali buti
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

            var UserIdByDash = _dashRepository.GetUserByDashId(request.Item.DashBoardId);
            var UserIdBySessionId = _userSessionRepository.GetUserBySessionId(request.Ticket);

            if (UserIdBySessionId != null && UserIdByDash != null && UserIdBySessionId.Id != UserIdByDash.Id) //TODO pasiklausti zilvino ar good practice
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACCESS"));
            }

            if (!IsSessionValid(request.Ticket))
            {
                errors.Add(new ErrorStatus("SESSION_EXPIRED"));
            }

            return errors;
        }
    }
}
