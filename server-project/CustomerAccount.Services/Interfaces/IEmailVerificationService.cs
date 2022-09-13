namespace CustomerAccount.Services.Interfaces
{
    public interface IEmailVerificationService
    {
        Task AddEmailVerification(string emailVerificationAddress);
        Task SendEmail(string email, string verificationCode);
    }
}