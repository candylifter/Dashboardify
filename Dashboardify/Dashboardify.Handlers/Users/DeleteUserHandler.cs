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
       
        public DeleteUserHandler(string connectionString):base (connectionString)
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
                _userRepository.DeleteUser(request.UserId);

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

           return errors;
        }
    }
}
