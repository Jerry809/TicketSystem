using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace TicketSystem.API.Extensions
{
    public static class PasswordHashExtension
    {
        private const string _salt = "rnk93nbasdk1#D";
        
        public static string GetHashPassword(string password)
        {
            var saltBytes = Encoding.UTF8.GetBytes(_salt);

            var hashBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8
            );

            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }
    }
}