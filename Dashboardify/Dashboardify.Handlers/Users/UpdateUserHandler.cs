using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Users;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Users
{
    public class UpdateUserHandler:BaseHandler
    {
        private UsersRepository _usersRepository;

        private UserSessionRepository _userSessionRepository;

        public UpdateUserHandler(string connectionString):base(connectionString)
        {
            _usersRepository = new UsersRepository(connectionString);

            _userSessionRepository = new UserSessionRepository(connectionString);
        }

        public UpdateUserResponse Handle(UpdateUserRequest request)
        {
            var response = new UpdateUserResponse();

            response.Errors = Validate(request);
                        
            if (response.HasErrors)
            {
                return response;
            }

            var OriginUser = _userSessionRepository.GetUserBySessionId(request.Ticket);

            if (!string.IsNullOrEmpty(request.Password)) //nes gali ir nepaduoti
            {
                request.Password = Helpers.PasswordsHelper.HashPassword(request.Password);
            }

            try
            {
                UpdateUserObject(OriginUser, request.Username,request.Password,request.Email);
                
                _usersRepository.Update(OriginUser);

                return response;
            }
            catch (Exception)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));

                return response;
            }

            
        }

        private void UpdateUserObject(User origin, string name, string password, string email)
        {
            
            if (!string.IsNullOrEmpty(name)) { 
                origin.Name = name;
            }

            if (!string.IsNullOrEmpty(email))
            {
                origin.Email = email;
            }

            if (!string.IsNullOrEmpty(password))
            {
                origin.Password = password;
            }
        }

        private IList<ErrorStatus> Validate(UpdateUserRequest request)
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

            var user = _userSessionRepository.GetUserBySessionId(request.Ticket);

            if (user==null) 
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
                return errors;
            }            
            
            
            return errors;
        }
    }
}
