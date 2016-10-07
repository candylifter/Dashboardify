using System;
using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Users;
using Dashboardify.Handlers.Helpers;
using Dashboardify.Models;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Users
{
    public class UpdateUserHandler : BaseHandler
    {
        private UsersRepository _usersRepository;
        
        public UpdateUserHandler(string connectionString) : base(connectionString)
        {
            _usersRepository = new UsersRepository(connectionString);

        }

        public UpdateUserResponse Handle(UpdateUserRequest request)
        {
            var response = new UpdateUserResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }

            
            if (!string.IsNullOrEmpty(request.UserToUpdate.Password)) //nes gali ir nepaduoti
            {
                request.UserToUpdate.Password = PasswordsHelper.HashPassword(request.UserToUpdate.Password);
            }

            try
            {
                
                UpdateUserObject(request.UserOrigin, request.UserToUpdate);

                _usersRepository.Update(request.UserOrigin);

                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ErrorStatus(ex.Message));

                //response.Errors.Add(new ErrorStatus("BAD_REQUEST"));

                return response;
            }


        }

        private void UpdateUserObject(User origin, User updated)
        {

            if (!string.IsNullOrEmpty(updated.Name))
            {
                origin.Name = updated.Name;
            }

            if (!string.IsNullOrEmpty(updated.Email))
            {
                origin.Email = updated.Email;
            }

            if (!string.IsNullOrEmpty(updated.Password))
            {
                origin.Password = updated.Password;
            }
        }

        private IList<ErrorStatus> Validate(UpdateUserRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (request.UserToUpdate == null)
            {
                errors.Add(new ErrorStatus("BAD_REQUEST"));
                return errors;
            }

            if (!string.IsNullOrEmpty(request.UserToUpdate.Name))
            {
                if (request.UserToUpdate.Name.Length > 254)
                {
                    errors.Add(new ErrorStatus("USERNAME_TOO_LONG"));
                    return errors;
                }
            }
            
            if (request.UserToUpdate.Id != request.UserOrigin.Id)
            {
                errors.Add(new ErrorStatus("UNAUTHORIZED_ACCESS"));
            }

            if (!string.IsNullOrEmpty(request.UserToUpdate.Email))
            {
                if (request.UserOrigin.Email != request.UserToUpdate.Email)
                    {
                        if (request.UserToUpdate.Email.Length > 254)
                        {
                        errors.Add(new ErrorStatus("EMAIL_TOO_LONG"));
                        return errors;
                    }

                    if (!request.UserToUpdate.Email.Contains("@"))
                        {
                            errors.Add(new ErrorStatus("BAD_EMAIL_FORMAT"));
                            return errors;
                        }
                        if (_usersRepository.CheckIfEmailAvailable(request.UserToUpdate.Email))
                        {
                            errors.Add(new ErrorStatus("EMAIL_ALREADY_TAKEN"));
                            return errors;
                        }

                    }
            }
            

            return errors;
        }
    }
}
