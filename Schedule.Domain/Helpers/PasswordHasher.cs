using System.Security.Cryptography;
using System.Text;


namespace Schedule.domain.Helpers
{
    public static class PasswordHasher
    {
        public static string GenerateHasher(string password) //length of salt    
        {
            var md5 = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) 
                sb.Append(hash[i].ToString("X2"));
                    
            return sb.ToString();
        }     
    }
}
