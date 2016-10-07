using System;
using System.Collections.Generic;
using System.Configuration;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Dashboards;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Dashboards;
using Dashboardify.Handlers.Helpers;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Users
{
    public class CreateUserHandler:BaseHandler
    {
        private UsersRepository _userRepository;

        private DashRepository _dashRepository;

        public CreateUserHandler(string connectionString):base(connectionString)
        {
            _userRepository = new UsersRepository(connectionString);

            _dashRepository = new DashRepository(connectionString);
        }
        /// <summary>
        /// Creates User and default dash
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CreateUserResponse Handle(CreateUserRequest request)
        {
            var response = new CreateUserResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }
            try
            {

                var currentDate = DateTime.Now;
                
                request.Password = PasswordsHelper.HashPassword(request.Password);

                int User_Id=_userRepository.CreateUserAndGetHisId(new User()
                {
                    DateModified = currentDate,
                    DateRegistered = currentDate,
                    Name = request.Username,
                    Password = request.Password,
                    Email = request.Email,
                    IsActive = true
                });

                EmailHelper.SendEmail(request);

                response.UserId = User_Id;
                
                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));
                //response.Errors.Add(new ErrorStatus(ex.Message));

                return response;
            }
            
        }

        public IList<ErrorStatus> Validate(CreateUserRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (IsRequestNull(request))
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.InvitationCode))
            {
                errors.Add(new ErrorStatus("WRONG_INPUT"));
                return errors;
            }

            if (request.Email.Contains("one.lt") || !request.Email.Contains("@"))
            {
                errors.Add(new ErrorStatus("WRONG_EMAIL_FORMAT"));
                return errors;
            }

            if (!string.IsNullOrEmpty(_userRepository.ReturnEmail(request.Email)))
            {
                errors.Add(new ErrorStatus("EMAIL_ALREADY_TAKEN"));
                return errors;
            }
            if (request.InvitationCode != ConfigurationManager.AppSettings["InvitationCode"])
            {
                errors.Add(new ErrorStatus("INVITATION_CODE_DONT_MATCH"));
                return errors;
            }
            
            return errors;
        }

      
      
    }
    
}
