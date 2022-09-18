using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomExceptions;
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
        try
        {
            await _contect.EmailVerifications.AddAsync(emailVerification);
            await _contect.SaveChangesAsync();
        }
        catch
        {
            throw new NotSavedException("faild to save new emailVerfication");
        }
    }
    public async Task UpdateEmailVerification(EmailVerification emailVerification)
    {
        using var _contect = _contextFactory.CreateDbContext();
        try
        {
            _contect.EmailVerifications.Update(emailVerification);
            await _contect.SaveChangesAsync();
        }
        catch
        {
            throw new NotSavedException("faild to save update for emailVerfication");
        }
    }
    public async Task<bool> CheckEmailVerifiedByEmail(string email)
    {
        using var _contect = _contextFactory.CreateDbContext();
        EmailVerification? emailVerification;
        try
        {
             emailVerification = await _contect.EmailVerifications.Where(em => em.Email.Equals(email)).FirstOrDefaultAsync();
        }
        catch
        {
            throw new NoAccessException("no access to Check EmailVerified By Email");
        }
        if(emailVerification is null)
        {
            return false;
        }
        return true;
    }
    public async Task<bool> CheckVerification(string email, string verifiCode)
    {
        using var _context = _contextFactory.CreateDbContext();
        DateTime dateTime = DateTime.UtcNow;
        EmailVerification? emailVerification = await _context.EmailVerifications.
            Where((em) => em.Email.Equals(email) && em.VerificationCode.Equals(verifiCode)
            && em.ExpirationTime >= dateTime).
            FirstOrDefaultAsync();
        if (emailVerification is null)
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

