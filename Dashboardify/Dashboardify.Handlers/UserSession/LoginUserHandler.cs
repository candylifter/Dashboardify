using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Dashboardify.Contracts;
using Dashboardify.Contracts.UserSession;
using Dashboardify.Models;
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
            var respone = new LoginUserResponse();

            request.user.Password = HashPassword(request.user.Password);

            respone.Errors = Validate(request);
            if (respone.HasErrors)
            {
                return respone;
            }
 
            var originUser = _usersRepository.ReturnIfExsists(request.user.Name, request.user.Password);
                
            AddSession(originUser);
            
            return respone;
            

        }

        private IList<ErrorStatus> Validate(LoginUserRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (request.user.Name == "" || request.user.Password == "")
            {
                errors.Add(new ErrorStatus("WRONG_INPUT"));
            }
            if (_usersRepository.ReturnIfExsists(request.user.Name, request.user.Password) == null)
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
            }
            return errors;
            
        }

        private void AddSession(User user)
        {
            String guid = Guid.NewGuid().ToString();
            var session = new Models.UserSession()
            {
                Expires = DateTime.Now.AddMinutes(20),
                UserId = user.Id,
                Id = guid
            };
            
            if(_userSessionRepository.AddSession(session))// add sesion yra bool (grazina true arba false)
            {
                
            }
            else
            {
                throw new Exception("Duplicate of session id");  
            }
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
