using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.options;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CustomerAccount.Services.Services;

public class EmailVerificationService : IEmailVerificationService
{
    private readonly IAccountDal _accountDal;
    private readonly IOptions<Email> _emailAccount;
    private readonly IEmailVerificationDal _emailVerificationDal;
    public EmailVerificationService(IEmailVerificationDal emailVerificationDal, IAccountDal accountDal, IOptions<Email> emailAccout)
    {
        _emailVerificationDal = emailVerificationDal;
        _accountDal = accountDal;
        _emailAccount = emailAccout;
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
        string from = _emailAccount.Value.emailAddress;
        string password =_emailAccount.Value.password;
        MailMessage message = new MailMessage();
        message.From = new MailAddress(from);
        message.Subject = "CrossRiverBank  your verification code is below";
        message.To.Add(new MailAddress(email));
        message.Body = "<html><body> " + verificationCode + " </body></html>";
        message.IsBodyHtml = true;
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(from, password),
            EnableSsl = true,
        };
        smtpClient.Send(message); ;
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

