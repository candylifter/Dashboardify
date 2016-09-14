using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Users;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Users
{
    public class DeleteUserHandler
    {
        private UsersRepository _userRepository;
        public DeleteUserHandler(string connectionString)
        {
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
                _userRepository.DeleteUser(request.User.Id);

                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus(ex.Message));
                return response;
            }
            

        }

        public IList<ErrorStatus> Validate(DeleteUserRequest request)
        {
            var errors = new List<ErrorStatus>();
            if (request.User.Id < 1)
            {
                errors.Add(new ErrorStatus("INVALID_ID"));
                return errors;
            }
            if (request.User.Id.ToString() == "")
            {
                errors.Add(new ErrorStatus("ID_NOT_RECIEVED"));
            }
            return errors;
        }
    }
}
