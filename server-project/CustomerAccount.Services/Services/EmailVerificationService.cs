using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomerAccount.Services.Interfaces;
using CustomExceptions;
using System.Net;
using System.Net.Mail;
using System.Text;

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
    public async Task AddEmailVerification(String emailVerificationAddress)
    {
        try
        {
            //check that this  mail is not  in use
            bool exists = await _accountDal.EmailExists(emailVerificationAddress);
            if (!exists)
            {
                //check if this email tried allready to verifi
                bool emailVerified = await _emailVerificationDal.CheckEmailVerifiedByEmail(emailVerificationAddress);
                //Guid verificationCode = Guid.NewGuid();
               //string verifiCode=verificationCode.ToString(); 
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
        /*MailAddress from = new MailAddress("212648802@mby.co.il");
        MailAddress to = new MailAddress(email);
        MailMessage mail = new MailMessage(from, to);
        SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
        mail.Subject = "CrossRiverBank  your verification code is below";
        mail.Body = verificationCode;
        SmtpServer.Port = 587;
        //password
        SmtpServer.Credentials = new System.Net.NetworkCredential("212648802@mby.co.il", "Student@264");
        SmtpServer.EnableSsl = true;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Send(mail);*/
        string fromMail = "sendemail081@gmail.com";
        string fromPassword = "qsszgtsvvsukdxay";
        MailMessage message = new MailMessage();
        message.From = new MailAddress(fromMail);
        message.Subject = "CrossRiverBank  your verification code is below";
        message.To.Add(new MailAddress(email));
        message.Body = "<html><body> " + verificationCode + " </body></html>";
        message.IsBodyHtml = true;
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromMail, fromPassword),
            EnableSsl = true,
        };
        smtpClient.Send(message);

    }
    public async Task<bool> CheckVerification(string email, string verifiCode)
    {
        return await _emailVerificationDal.CheckVerification(email, verifiCode);

    }
    public Task<int> DeleteExpiredCodes()
    {
       return _emailVerificationDal.DeleteExpiredCodes();
    }

}

