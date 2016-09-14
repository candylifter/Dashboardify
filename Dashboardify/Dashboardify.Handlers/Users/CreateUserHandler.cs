using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Users;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Users
{
    public class CreateUserHandler
    {
        private UsersRepository _userRepository;

        public CreateUserHandler(string connectionString)
        {
            _userRepository = new UsersRepository(connectionString);
        }

        public CreateUserResponse Handle(CreateUserRequest request)
        {
            var response = new CreateUserResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }
            try
            {
                request.Password = HashPassword(request.Password);

                _userRepository.CreateUser(new User()
                {
                    DateModified = DateTime.Now,
                    DateRegistered = DateTime.Now,
                    Name = request.Username,
                    Password = request.Password,
                    Email = request.Email,
                    IsActive = true

                });

                SendEmail(request);


                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus(ex.Message));
                return response;
            }
            
        }

        public IList<ErrorStatus> Validate(CreateUserRequest request)
        {
            var errors = new List<ErrorStatus>();
            if (request.Username == "" || request.Email == "" || request.Password == "")
            {
                errors.Add(new ErrorStatus("WRONG_INPUT"));    
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

        private void SendEmail(CreateUserRequest request)
        {
            var fromAddress = new MailAddress("dashboardifyacademy@gmail.com", "Dashboardify");
            var toAddress = new MailAddress(request.Email, request.Username);
            const string fromPassword = "desbordas";
            const string subject = "Welcome";
            string body = "Dear "+ request.Username.ToString() +"\n We are happy that you are using our dashboardify app. (ITERPTI MAESTRO TRUMPA)";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
    
}
