using CustomerAccount.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;



namespace CustomerAccount.Services.Services;

public class PasswordHashService: IPasswordHashService
{
    public string GenerateSalt(int nSalt)
    {
        byte[] saltBytes = new Byte[nSalt];

        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        rng.GetNonZeroBytes(saltBytes); // The array is now filled with cryptographically strong random bytes, and none are zero.

        return Convert.ToBase64String(saltBytes);
    }

    public string HashPassword(string password, string salt, int nIterations, int nHash)
    {
        var saltBytes = Convert.FromBase64String(salt);
        //Iteration count is the number of times an operation is performed
        using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations))
        {
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
        }
    }

    public static byte[] GetHash(string password, string salt)
    {
        byte[] unhashedBytes = Encoding.Unicode.GetBytes(String.Concat(salt, password));

        SHA256Managed sha256 = new SHA256Managed();
        byte[] hashedBytes = sha256.ComputeHash(unhashedBytes);

        return hashedBytes;
    }

    public static bool CompareHash(string attemptedPassword, byte[] hash, string salt)
    {
        string base64Hash = Convert.ToBase64String(hash);
        string base64AttemptedHash = Convert.ToBase64String(GetHash(attemptedPassword, salt));

        return base64Hash == base64AttemptedHash;
    }
}


