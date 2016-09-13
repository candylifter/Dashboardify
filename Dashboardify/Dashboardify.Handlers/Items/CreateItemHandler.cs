﻿using System;
using System.Collections.Generic;
using Dashboardify.Repositories;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Items;

namespace Dashboardify.Handlers.Items
{
    public class CreateItemHandler
    {
        private ItemsRepository _itemRepository;

        public CreateItemHandler(String ConnectionString)
        {
            _itemRepository = new ItemsRepository(ConnectionString);
        }

        public CreateItemResponse Handle (CreateItemRequest request)
        {
            var response = new CreateItemResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }

            request.Item.IsActive = true;
            request.Item.Created = DateTime.Now;
            request.Item.Modified = DateTime.Now;
            request.Item.LastChecked = DateTime.Now;
            request.Item.Content = "";

            _itemRepository.Create(request.Item);

            response.Success = true;

            return response;
        }

        private IList<ErrorStatus> Validate(CreateItemRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (request.Item.DashBoardId == 0)
            {
                errors.Add(new ErrorStatus("DASHBOARDID_NOT_DEFINED"));
            }

            if (request.Item.CheckInterval < 30000) //neigiamas irgi negali buti
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

            return errors;
        }
    }
}
