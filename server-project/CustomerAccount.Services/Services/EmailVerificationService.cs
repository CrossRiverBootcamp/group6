using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomerAccount.Services.Interfaces;
using CustomExceptions;
using System.Net.Mail;

namespace CustomerAccount.Services.Services;

public class EmailVerificationService : IEmailVerificationService
{
    private readonly IAccountDal _accountDal;
    private readonly IEmailVerificationDal _emailVerificationDal;
    public EmailVerificationService(IEmailVerificationDal emailVerificationDal, IAccountDal accountDal)
    {
        _emailVerificationDal = emailVerificationDal;
        _accountDal = accountDal;
    }
    public async Task<bool> AddEmailVerification(String emailVerificationAddress)
    {
        //check that this email is not  in use
        bool exists = await _accountDal.EmailExists(emailVerificationAddress);
        if (exists)
        {
            return false;
        }
        //check if this email tried allready to verified
        bool emailVerified = await _emailVerificationDal.CheckEmailVerifiedByEmail(emailVerificationAddress);
        var code = new Random(Guid.NewGuid().GetHashCode()).Next(0, 9999).ToString("D4");
        EmailVerification emailVerification = new EmailVerification()
        {
            Email = emailVerificationAddress,
            VerificationCode = code,
            ExpirationTime = DateTime.UtcNow.AddMinutes(10),
        };
        if (!emailVerified)
        {
            await _emailVerificationDal.AddEmailVerification(emailVerification);
        }
        else
        {
            await _emailVerificationDal.UpdateEmailVerification(emailVerification);
        }
        await this.SendEmail(emailVerificationAddress, code);
        return true;



    }
    public async Task SendEmail(string email, string verificationCode)
    {
        String emailAddress = new string(email);
        MailAddress from = new MailAddress("212648802@mby.co.il");
        MailAddress to = new MailAddress(emailAddress);
        MailMessage mail = new MailMessage(from, to);
        SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
        mail.Subject = "CrossRiverBank  your verification code is below";
        mail.Body = verificationCode;
        SmtpServer.Port = 587;
        SmtpServer.Credentials = new System.Net.NetworkCredential("212648802@mby.co.il", "Student@264");
        SmtpServer.EnableSsl = true;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Send(mail);
    }
    public Task<bool> CheckVerification(string email, string verifiCode)
    {
        return _emailVerificationDal.CheckVerification(email, verifiCode);
    }
    public Task<int> DeleteExpiredCodes()
    {
        return _emailVerificationDal.DeleteExpiredCodes();
    }

}

