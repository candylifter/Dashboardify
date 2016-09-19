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
            AddSession(request);

            return response;
        }


        private IList<ErrorStatus> Validate(LoginUserRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (request.User.Email == "" || request.User.Password == "")
            {
                errors.Add(new ErrorStatus("WRONG_INPUT"));
            }
            if (_usersRepository.ReturnIfExsists(request.User.Email, request.User.Password) == null)
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
            }
            return errors;

        }

        private void AddSession(LoginUserRequest request)
        {
            var user = _usersRepository.ReturnIfExsists(request.User.Email, request.User.Password);

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
