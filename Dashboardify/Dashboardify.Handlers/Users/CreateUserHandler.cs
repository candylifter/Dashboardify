using System;
using System.Collections.Generic;
using System.Configuration;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Helpers;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Users
{
    public class CreateUserHandler:BaseHandler
    {
        private UsersRepository _userRepository;


        public CreateUserHandler(string connectionString):base(connectionString)
        {
            _userRepository = new UsersRepository(connectionString);
        }
        
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

                int userId=_userRepository.CreateUserAndGetHisId(new User()
                {
                    DateModified = currentDate,
                    DateRegistered = currentDate,
                    Name = request.Username,
                    Password = request.Password,
                    Email = request.Email,
                    IsActive = true
                });

                EmailHelper.SendEmail(request);

                response.UserId = userId;
                
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

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.InvitationCode))
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
            if (request.Email.Length > 254 || request.Email.Length < 3)
            {
                errors.Add(new ErrorStatus("WRONG_EMAIL_FORMAT"));
                return errors;
            }
            if (request.Username.Length < 5 || request.Username.Length > 254)
            {
                errors.Add(new ErrorStatus("USERNAME_MUST_BE_ATLEAST_5_CHARACTERS_LONG"));
                return errors;
            }
            
            return errors;
        }

      
      
    }
    
}
