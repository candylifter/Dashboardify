using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Dashboardify.Contracts;
using Dashboardify.Contracts.UserSession;
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

            request.User.Password = HashPassword(request.User.Password);

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

            if (string.IsNullOrEmpty(request.User.Name) || string.IsNullOrEmpty(request.User.Password))
            {
                errors.Add(new ErrorStatus("WRONG_INPUT"));
            }
            if (_usersRepository.ReturnIfExsists(request.User.Name, request.User.Password) == null)//TODO pasiklausti zilvino
            {
                errors.Add(new ErrorStatus("INVALID_USERNAME_OR_PASSWORD"));
            }
            return errors;

        }

        private void AddSession(LoginUserRequest request, LoginUserResponse response)
        {
            var user = _usersRepository.ReturnIfExsists(request.User.Name, request.User.Password);

            var session1 = Guid.NewGuid().ToString().Replace("-", "");

            var session2 =  Guid.NewGuid().ToString().Replace("-", "");
            
            var session3 =  Guid.NewGuid().ToString().Replace("-", "");

            var session4 = Guid.NewGuid().ToString().Replace("-", "");

            var sessionId = session1 + session2 + session3 + session4;

            var session = new Models.UserSession()
            {
                Expires = DateTime.Now.AddMinutes(20),
                UserId = user.Id,
                SessionId = sessionId
            };
            
            _userSessionRepository.AddSession(session);
            response.SessionId = sessionId;
        }

        private string HashPassword(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
