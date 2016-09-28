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

            
            try
            {
                UpdateUserObject(OriginUser, request.User);

                OriginUser.Name = request.User.Name;

                _usersRepository.Update(OriginUser);

                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus(ex.Message));
                return response;
            }

            
        }

        private void UpdateUserObject(User origin, User updated)
        {
            if (!string.IsNullOrEmpty(updated.Name)) { 
                origin.Name = updated.Name;
            }

            if (!string.IsNullOrEmpty(updated.Email))
            {
                origin.Email = updated.Email;
            }

            if (!string.IsNullOrEmpty(updated.Password))
            {
                origin.Password = updated.Password;
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

            if (request.User == null) 
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));

                return errors;
            }

            if (string.IsNullOrEmpty(request.Ticket))
            {
                errors.Add(new ErrorStatus("INVALID_TICKET"));
                return errors;
            }

            if (request.User.Id < 1)
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
  
            }            

            if (!string.IsNullOrEmpty(request.User.Email) && request.User.Email.Contains("one.lt"))
            {
                errors.Add(new ErrorStatus("EMAIL_WRONG_FORMAT"));
                return errors;
            }

            var user = _userSessionRepository.GetUserBySessionId(request.Ticket);

            if (user == null)
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
                return errors;
            }

            return errors;
        }
    }
}
