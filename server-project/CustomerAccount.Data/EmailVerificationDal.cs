using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Data;

public class EmailVerificationDal : IEmailVerificationDal
{
    private readonly IDbContextFactory<CustomerAccountContext> _contextFactory;
    public EmailVerificationDal(IDbContextFactory<CustomerAccountContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    public async Task AddEmailVerification(EmailVerification emailVerification)
    {
        using var _contect = _contextFactory.CreateDbContext();
        await _contect.EmailVerifications.AddAsync(emailVerification);
        try
        {
            await _contect.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public async Task UpdateEmailVerification(EmailVerification emailVerification)
    {
        using var _contect = _contextFactory.CreateDbContext();
         _contect.EmailVerifications.Update(emailVerification);
        try
        {
            await _contect.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public async Task<EmailVerification> GetEmailVerificationByEmail(string email)
    {
        using var _contect = _contextFactory.CreateDbContext();
        EmailVerification emailVerification= await _contect.EmailVerifications.Where(em => em.Equals(email)).FirstOrDefaultAsync();
        return emailVerification;
    }
   

}

