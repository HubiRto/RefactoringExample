using System;
using LegacyApp;

namespace LegacyAppConsumer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            /*
             * DO NOT CHANGE THIS FILE AT ALL
             */

            var userService = new UserService(new ClientRepository(), new UserCreditService());
            var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
            Console.WriteLine(addResult ? $"Adding John Doe was successful" : $"Adding John Doe was unsuccessful");
        }
    }
}