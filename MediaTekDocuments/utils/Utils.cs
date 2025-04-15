using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MediaTekDocuments.utils
{
    public class Utils
    {
        /// <summary>
        /// Permet de hasher le password
        /// Trouvée sur le site : https://www.restack.io/p/secure-hashing-techniques-answer-csharp-hash-password-sha256-example-cat-ai
        /// </summary>
        /// <param name="password">Mot de passe à hasher</param>
        /// <returns>Le mot de passe hashé</returns>
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Compare deux mots de passe pour vérifier leur correspondance
        /// </summary>
        /// <param name="inputPassword">le mot de passe saisi sans hash</param>
        /// <param name="storedHash">le mot de passe stocké en base de données (hashé)</param>
        /// <returns>True si les mots de passe correspondent</returns>
        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashOfInput = HashPassword(inputPassword);
            return hashOfInput.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
