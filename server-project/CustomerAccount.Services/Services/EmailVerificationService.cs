using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomerAccount.Services.Exceptions;
using CustomerAccount.Services.Interfaces;
using System.Net.Mail;

namespace CustomerAccount.Services.Services;

public class EmailVerificationService : IEmailVerificationService
{
    private IAccountDal _accountDal;
    private readonly IEmailVerificationDal _emailVerificationDal;
    public EmailVerificationService(IEmailVerificationDal emailVerificationDal, IAccountDal accountDal)
    {
        _emailVerificationDal = emailVerificationDal;
        _accountDal = accountDal;
    }
    public async Task AddEmailVerification(String emailVerificationAddress)
    {
        try
        {
            //check that this  mail is not  in use
            bool exists = await _accountDal.EmailExists(emailVerificationAddress);
            if (!exists)
            {
                //check if this email tried allready to verifi
                EmailVerification emailVerification = await _emailVerificationDal.GetEmailVerificationByEmail(emailVerificationAddress);
                if (emailVerification == null)
                {
                    emailVerification = new EmailVerification()
                    {
                        Email = emailVerificationAddress,
                        VerificationCode = "1234",
                        ExpirationTime = DateTime.UtcNow.AddMinutes(10),
                    };

                    await _emailVerificationDal.AddEmailVerification(emailVerification);
                }
                await _emailVerificationDal.UpdateEmailVerification(emailVerification);
                await this.SendEmail(emailVerificationAddress, "1234");
            }
            else
            {
                throw new DuplicatedException("email address in use!!");
            }
        }
        catch (Exception ex)
        {
              throw ex;
        }

    }
    public async Task SendEmail(string email, string verificationCode)
    {
     
        MailAddress from = new MailAddress("324103357@mby.co.il"); 
        MailAddress to = new MailAddress(email);
        MailMessage mail = new MailMessage(from, to);
        SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
        mail.Subject = "CrossRiverBank  your verification code is below";
        mail.Body = verificationCode;
        SmtpServer.Port = 587;
        SmtpServer.Credentials = new System.Net.NetworkCredential("324103357@mby.co.il", "Student@264");
        SmtpServer.EnableSsl = true;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Send(mail);
    }
}

