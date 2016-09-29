using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Users;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Users
{
    public class DeleteUserHandler:BaseHandler
    {
        private UsersRepository _userRepository;
        private UserSessionRepository _userSessionRepository;
        public DeleteUserHandler(string connectionString):base (connectionString)
        {
            _userSessionRepository = new UserSessionRepository(connectionString);
            _userRepository = new UsersRepository(connectionString);
            
        }

        public DeleteUserResponse Handle(DeleteUserRequest request)
        {
            var response = new DeleteUserResponse();
            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }
            try
            {
                var user = _userSessionRepository.GetUserBySessionId(request.Ticket);

                _userRepository.DeleteUser(user.Id);

                return response;
            }
            catch (Exception)
            {
                response.Errors.Add(new ErrorStatus("BAD_REQUEST"));

                return response;
            }
            

        }

        public IList<ErrorStatus> Validate(DeleteUserRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (IsRequestNull(request))
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }

            if (string.IsNullOrEmpty(request.Ticket))
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
            }

            var user = _userSessionRepository.GetUserBySessionId(request.Ticket);

            if (IsRequestNull(user))
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));

            }
            
           
            return errors;
        }
    }
}
