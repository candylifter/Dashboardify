using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dashboardify.Contracts;
using Dashboardify.Contracts.UserSession;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.UserSession
{
    public class LoginUserHandler
    {
        private Guid _guid;

        private UsersRepository _usersRepository;

        private UserSessionRepository _userSessionRepository;

        public LoginUserHandler(string connectionString)
        {
            _usersRepository = new UsersRepository(connectionString);
            _userSessionRepository = new UserSessionRepository(connectionString);
            _guid = new Guid();
        }

        public LoginUserResponse Handle(LoginUserRequest request)
        {
            var respone = new LoginUserResponse();

            respone.Errors = Validate(request);
            if (respone.HasErrors)
            {
                return respone;
            }

            request.user.Password = HashPassword(request.user.Password);


            var session = new Models.UserSession()
            {
                Expires = DateTime.Now.AddMinutes(20),
                UserId = request.user.Id,
                Id = _guid.ToString()
            };

            _userSessionRepository.AddSession(session);



            return respone;


        }

        private IList<ErrorStatus> Validate(LoginUserRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (request.user.Name == "" || request.user.Password == "")
            {
                errors.Add(new ErrorStatus("WRONG_INPUT"));
            }
            if (Regex.IsMatch(request.user.Name,"[*-/+" +"]"))
            {
                errors.Add(new ErrorStatus("INVALID_CHARACTER"));
            }
            if (_usersRepository.ReturnIfExsists(request.user.Name, request.user.Password) == null)
            {
                errors.Add(new ErrorStatus("USER_NOT_FOUND"));
            }
            return errors;
            
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
