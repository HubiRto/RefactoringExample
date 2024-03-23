using System;

namespace LegacyApp
{
    public class UserService(IClientRepository clientRepository, IUserCreditService userCreditService)
    {
        private static bool IsEmailCorrect(string email)
        {
            return email.Contains('@') && email.Contains('.');
        }

        private static bool IsFullNameEmpty(string firstName, string lastName)
        {
            return string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName);
        }
        
        

        public bool AddUser(
            string firstName,
            string lastName,
            string email,
            DateTime dateOfBirth,
            int clientId
        )
        {
            if (IsFullNameEmpty(firstName, lastName))
            {
                return false;
            }

            if (IsEmailCorrect(email))
            {
                return false;
            }

            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            switch (client.Type)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                {
                    var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                    break;
                }
                default:
                {
                    user.HasCreditLimit = true;
                    var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                    break;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}