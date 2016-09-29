﻿using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Items;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Items
{
    public class GetItemsListHandler : BaseHandler
    {
        private ItemsRepository _itemsRepository;
        private ScreenshotRepository _screenshotRepository;
        private UserSessionRepository _userSessionRepository;
        private DashRepository _dashRepository;

        public GetItemsListHandler(string connectionString) :
            base(connectionString)
        {
            _itemsRepository = new ItemsRepository(connectionString);
            _screenshotRepository = new ScreenshotRepository(connectionString);
            _userSessionRepository = new UserSessionRepository(connectionString);
            _dashRepository = new DashRepository(connectionString);
        }

        public GetItemsListResponse Handle(GetItemsListRequest request) //TODO recheck all items handlers
        {
            var response = new GetItemsListResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }
            try
            {
                var items = _itemsRepository.GetByDashboardId(request.DashboardId);

                var itemsWithScreenshots = new List<Item>();

                foreach (var item in items)
                {
                    var screenshot = _screenshotRepository.GetLastByItemId(item.Id);

                    if (screenshot != null)
                    {
                        item.Screenshots.Add(screenshot);
                    }

                    itemsWithScreenshots.Add(item);
                }

                response.Items = itemsWithScreenshots;

                return response;
            }
            catch (Exception)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                return response;
            }
        }

        private IList<ErrorStatus> Validate(GetItemsListRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (IsRequestNull(request))
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }
            
            if (string.IsNullOrEmpty(request.Ticket))
            {
                errors.Add(new ErrorStatus("TICKET_NOT_DEFINED"));
                return errors;
            }
            if (request.DashboardId < 1)
            {
                errors.Add(new ErrorStatus("CORRUPTED_ID"));
                return errors;
            }

            var requestUser = _userSessionRepository.GetUserBySessionId(request.Ticket);

            var ownerUser = _dashRepository.GetUserByDashId(request.DashboardId);

            if (requestUser == null || ownerUser == null )
            {
                errors.Add(new ErrorStatus("WRONG_REQUEST"));
                return errors;
            }

            if (!IsSessionValid(request.Ticket))
            {
                errors.Add(new ErrorStatus("SESSION_TIMEOUT"));
            }
            
            if (requestUser.Id != ownerUser.Id)
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACESS"));
                return errors;
            }



            return errors;
        }
    }
}
