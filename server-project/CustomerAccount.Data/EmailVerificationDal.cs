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
    public async Task<bool> CheckEmailVerifiedByEmail(string email)
    {
        using var _contect = _contextFactory.CreateDbContext();
        var emailVerification = await _contect.EmailVerifications.Where(em => em.Email.Equals(email)).FirstOrDefaultAsync();
        if(emailVerification == null)
        {
            return false;
        }
        return true;
    }
    public async Task<bool> CheckVerification(string email, string verifiCode)
    {
        using var _context = _contextFactory.CreateDbContext();
        DateTime dateTime = DateTime.UtcNow;
        EmailVerification emailVerification = await _context.EmailVerifications.
            Where((em) => em.Email.Equals(email) && em.VerificationCode.Equals(verifiCode)
            && em.ExpirationTime >= dateTime).
            FirstOrDefaultAsync();
        if (emailVerification == null)
        {
            return false;
        }
        return true;
    }
    public async Task<int> DeleteExpiredCodes()
    {
        using var _context = _contextFactory.CreateDbContext();
        int rowEffected = await _context.Database.ExecuteSqlRawAsync("exec cleanExpiredCodeRows");
        return rowEffected;
    }


}

