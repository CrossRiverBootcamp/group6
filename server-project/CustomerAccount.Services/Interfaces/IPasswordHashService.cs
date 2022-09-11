namespace CustomerAccount.Services.Interfaces;
public interface IPasswordHashService
{
    string GenerateSalt(int nSalt);
    string HashPassword(string password, string salt, int nIterations, int nHash);
}
