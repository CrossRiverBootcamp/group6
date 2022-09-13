using CustomerAccount.Data.Entities;

namespace CustomerAccount.Data.Interfaces
{
    public interface IEmailVerificationDal
    {
        Task AddEmailVerification(EmailVerification emailVerification);
        Task<EmailVerification> GetEmailVerificationByEmail(string email);
        Task UpdateEmailVerification(EmailVerification emailVerification);
    }
}