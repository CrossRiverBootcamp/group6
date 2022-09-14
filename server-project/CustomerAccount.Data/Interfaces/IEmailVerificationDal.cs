using CustomerAccount.Data.Entities;

namespace CustomerAccount.Data.Interfaces
{
    public interface IEmailVerificationDal
    {
        Task AddEmailVerification(EmailVerification emailVerification);
        Task<bool> CheckEmailVerifiedByEmail(string email);
        Task UpdateEmailVerification(EmailVerification emailVerification);
        Task<bool> CheckVerification(string email, string verifiCode);
    }
}