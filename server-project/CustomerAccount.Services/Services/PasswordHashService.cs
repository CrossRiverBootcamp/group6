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

}


