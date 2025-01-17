﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyApp
{
    public class UserCreditService : IDisposable, IUserCreditService
    {
        /// <summary>
        /// Simulating database
        /// </summary>
        private readonly Dictionary<string, int> _database =
            new()
            {
                { "Kowalski", 200 },
                { "Malewski", 20000 },
                { "Smith", 10000 },
                { "Doe", 3000 },
                { "Kwiatkowski", 1000 }
            };

        public void Dispose()
        {
            //Simulating disposing of resources
        }

        /// <summary>
        /// This method is simulating contact with remote service which is used to get info about someone's credit limit
        /// </summary>
        /// <returns>Client's credit limit</returns>
        public int GetCreditLimit(string lastName, DateTime dateOfBirth)
        {
            var randomWaitingTime = new Random().Next(3000);
            Thread.Sleep(randomWaitingTime);

            if (_database.TryGetValue(lastName, out var limit)) return limit;
            throw new ArgumentException($"Client {lastName} does not exist");
        }
    }
}