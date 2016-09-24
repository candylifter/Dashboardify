using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Dashboardify.Contracts;
using Dashboardify.Contracts.UserSession;
using Dashboardify.Handlers.Helpers;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.UserSession
{
    public class LoginUserHandler
    {

        private UsersRepository _usersRepository;

        private UserSessionRepository _userSessionRepository;

        public LoginUserHandler(string connectionString)
        {
            _usersRepository = new UsersRepository(connectionString);
            _userSessionRepository = new UserSessionRepository(connectionString);

        }

        public LoginUserResponse Handle(LoginUserRequest request)
        {
            var response = new LoginUserResponse();

            request.User.Password = PasswordsHelper.HashPassword(request.User.Password);

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }

            AddSession(request,response);

            return response;
        }


        private IList<ErrorStatus> Validate(LoginUserRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (string.IsNullOrEmpty(request.User.Email) || string.IsNullOrEmpty(request.User.Password))
            {
                errors.Add(new ErrorStatus("WRONG_INPUT"));
            }
            if (_usersRepository.ReturnIfExsists(request.User.Email, request.User.Password) == null)
            {
                errors.Add(new ErrorStatus("INVALID_USERNAME_OR_PASSWORD"));
            }
            return errors;
        }


        private string CreateSessionId()
        {
            var sessionId = $"{Guid.NewGuid()}{Guid.NewGuid()}{Guid.NewGuid()}{Guid.NewGuid()}";
            sessionId = sessionId.Replace("-", "");

            return sessionId;
        }

        private void AddSession(LoginUserRequest request, LoginUserResponse response)
        {
            var user = _usersRepository.ReturnIfExsists(request.User.Email, request.User.Password);

            var sessionId = CreateSessionId();

            var session = new Models.UserSession()
            {
                Expires = DateTime.Now.AddMinutes(20),
                UserId = user.Id,
                Ticket = sessionId
            };
            
            _userSessionRepository.AddSession(session);
            response.SessionId = sessionId;
        }

        
    }
}
