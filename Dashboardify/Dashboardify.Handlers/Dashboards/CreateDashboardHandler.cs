using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Dashboards
{
    public class CreateDashboardHandler:BaseHandler
    {
        private DashRepository _dashRepository;
        
        public CreateDashboardHandler(string connectionString) : base(connectionString)
        {
            _dashRepository = new DashRepository(connectionString);
        }

        public CreateDashboardResponse Handle(CreateDashboardRequest request)
        {
            var response = new CreateDashboardResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }
            try
            {

                if (request.DashName != "Hello_DashBoardify")
                {
                    int dashId = _dashRepository.CreateAndGetId(new DashBoard
                    {
                        Name = request.DashName,
                        UserId = request.UserId,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        IsActive = true
                    });

                   
                    response.Dashboard = _dashRepository.Get(dashId);

                }

                else
                {
                    _dashRepository.Create(new DashBoard
                    {
                        Name = request.DashName,
                        UserId = request.UserId,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now
                    });
                    
                }
                
            }
            catch (Exception ex)
            {
                //response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                response.Errors.Add(new ErrorStatus(ex.Message));
            }
            
            return response;
        }

        public List<ErrorStatus> Validate(CreateDashboardRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (string.IsNullOrEmpty(request.DashName))
            {
                errors.Add(new ErrorStatus("DASH_NAME_NOT_DEFINED"));
                return errors;
            }

            if (request.DashName.Length > 254)
            {
                errors.Add(new ErrorStatus("NAME_TOO_LONG"));
                return errors;
            }
            

            return errors;
        }

        
    }
}
