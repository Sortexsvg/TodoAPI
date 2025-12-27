using Microsoft.AspNetCore.Identity;

namespace TodoApi.Helpers
{
    public class PasswordHelper
    {
        private static readonly PasswordHasher<string> _hasher = new();

        public static string Hash(string password)
        {
            return _hasher.HashPassword(null!, password);
        }

        public static bool Verify(string hashedPassword, string inputPassword)
        {
            return _hasher.VerifyHashedPassword(
                null!,
                hashedPassword,
                inputPassword
            ) == PasswordVerificationResult.Success;
        }
    }
}
