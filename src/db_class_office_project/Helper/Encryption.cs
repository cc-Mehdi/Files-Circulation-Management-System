using System;
using System.Security.Cryptography;
using System.Text;

namespace db_class_office_project.Helper
{
    static class Encryption
    {
        public static string HashPassword(string password)
        {
            // Ensure the input password is not null
            if (password == null) throw new ArgumentNullException(nameof(password));

            // Use SHA256 to hash the password
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash to a hexadecimal string
                StringBuilder result = new StringBuilder();
                foreach (byte b in hashBytes)
                    result.Append(b.ToString("x2")); // Converts each byte to a lowercase hex string

                return result.ToString();
            }
        }
    }

}
