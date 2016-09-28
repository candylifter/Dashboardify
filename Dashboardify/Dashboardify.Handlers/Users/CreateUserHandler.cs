using System;
using System.Collections.Generic;
using System.Configuration;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Helpers;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Users
{
    public class CreateUserHandler:BaseHandler
    {
        private UsersRepository _userRepository;

        public CreateUserHandler(string connectionString):base(connectionString)
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
                request.Password = PasswordsHelper.HashPassword(request.Password);

                _userRepository.CreateUser(new User()
                {
                    DateModified = DateTime.Now,
                    DateRegistered = DateTime.Now,
                    Name = request.Username,
                    Password = request.Password,
                    Email = request.Email,
                    IsActive = true
                });

                EmailHelper.SendEmail(request);


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

            if (IsRequestNull(request))
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                errors.Add(new ErrorStatus("WRONG_INPUT"));
                return errors;
            }
            if (!string.IsNullOrEmpty(_userRepository.ReturnEmail(request.Email)))
            {
                errors.Add(new ErrorStatus("EMAIL_ALREADY_TAKEN"));
                return errors;
            }
            if (string.IsNullOrEmpty(request.InvitationCode))
            {
                errors.Add(new ErrorStatus("NO_INVITATION_CODE"));
                return errors;
            }
            
            

            

            //todo metodas kuris patikrina ar email neuzimtas ir name
            return errors;

        }
        

        //private void SendEmail(CreateUserRequest request)//TODO refactor to mailsender in service layer
        //{
        //    var fromAddress = new MailAddress("dashboardifyacademy@gmail.com", "Dashboardify");
        //    var toAddress = new MailAddress(request.Email, request.Username);
        //    const string fromPassword = "desbordas";
        //    const string subject = "Welcome";
        //    string body = "Dear "+ request.Username +"\n We are happy that you are using our dashboardify app. (ITERPTI MAESTRO TRUMPA)";

        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        //    };
        //    using (var message = new MailMessage(fromAddress, toAddress)
        //    {
        //        Subject = subject,
        //        Body = body
        //    })
        //    {
        //        smtp.Send(message);
        //    }
        //}
    }
    
}
