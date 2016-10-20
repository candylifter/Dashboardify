using System.Collections.Generic;
using Dashboardify.Contracts;
using Dashboardify.Contracts.Users;
using Dashboardify.Repositories;

namespace Dashboardify.Handlers.Users
{
    public class ForgotPasswordHandler:BaseHandler
    {
        private UsersRepository _usersRepository;

        public ForgotPasswordHandler(string connectionString):base (connectionString)
        {
            _usersRepository = new UsersRepository(connectionString);
        }

        public ForgotPasswordResponse Handle(ForgotPasswordRequest request)
        {
            var response = new ForgotPasswordResponse();

            response.Errors = Validate(request);

            if (response.HasErrors)
            {
                return response;
            }


            // updatinti user pass i guida isiusti emaila
            //TODO repositorijoje parasyti metoda, kad gautu pagal emaila ir switchintu passworda bet cia dar ir daugiau xujnios


            return response;
        }

        private IList<ErrorStatus> Validate(ForgotPasswordRequest request)
        {
            var errors = new List<ErrorStatus>();

            if (string.IsNullOrEmpty(request.Email))
            {
                errors.Add(new ErrorStatus("EMAIL_NOT_FOUND"));
            }

            if (_usersRepository.CheckIfEmailAvailable(request.Email))
            {
                errors.Add(new ErrorStatus("EMAIL_NOT_FOUND"));
            }


            return errors;
        }
    }
}
