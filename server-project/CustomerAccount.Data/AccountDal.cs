using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomExceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Data;

public class AccountDal : IAccountDal
{
    private IDbContextFactory<CustomerAccountContext> _contextFactory;

    public AccountDal(IDbContextFactory<CustomerAccountContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<bool> CreateAccount(Account account, Customer customer)
    {
        using var _contect = _contextFactory.CreateDbContext();
        try
        {
            await _contect.Customers.AddAsync(customer);
            await _contect.Accounts.AddAsync(account);
            await _contect.SaveChangesAsync();
        }
        catch
        {
            throw new NotSavedException("create account faild");
        }
        return true;

    }

    public async Task<bool> EmailExists(string email)
    {
        using var _contect = _contextFactory.CreateDbContext();
        var exist = await _contect.Customers.Where((c) => c.Email.Equals(email)).FirstOrDefaultAsync();
        if (exist == null)
        {
            return false;
        }
        return true;
    }



    public async Task<Account> GetAccountInfo(int accountID)
    {
        using var _contect = _contextFactory.CreateDbContext();
        Account? account;
        try
        {
           account = await _contect.Accounts.FirstOrDefaultAsync(c => c.ID == accountID);       
        }
        catch
        {
            throw new NoAccessException("couldnt get account by accountID");
        }

        if (account is null)
        {
            throw new KeyNotFoundException("fail to get accountInfo by accountID");
        }
        return account;
    }


    public async Task<Account> FindUpdateAccount(int ID)
    {
        using var _contect = _contextFactory.CreateDbContext();
        Account acccountToUpDate = await _contect.Accounts.Where(a => a.ID == ID).FirstOrDefaultAsync();
        if (acccountToUpDate is null)
        {
            return null;
        }
        return acccountToUpDate;

    }
    public async Task<string> UpdateAccounts(Account accountFrom, Account accountTo)
    {
        using var _contect = _contextFactory.CreateDbContext();
        try
        {
            if (accountFrom == null || accountTo == null)
            {
                return "not enough details";
            }
            _contect.Accounts.Update(accountTo);
            _contect.Accounts.Update(accountFrom);
            await _contect.SaveChangesAsync();
            return null;
        }
        catch
        {
            return "the transaction systen had a temporry bug try again later";
        }




    }

    public async Task<string> GetSaltByEmail(string email)
    {
        using var _context = _contextFactory.CreateDbContext();
        string? salt;
        try
        {
            Customer? customer = await _context.Customers.Where(c => c.Email.Equals(email)).FirstOrDefaultAsync();
            salt = customer?.Salt;
        }
        catch
        {
            throw new NoAccessException("couldnt get customer by email");
        }
        return salt;

    }
    public async Task<int?> Login(string email, string password)
    {
        using var _context = _contextFactory.CreateDbContext();
        Account? account;
        try
        {
            account = await _context.Accounts.Where(c => c.Customer.Email.Equals(email) && c.Customer.Password.Equals(password)).FirstOrDefaultAsync();
        }
        catch
        {
            throw new NoAccessException("couldnt get account by email and password");
        }
        return account?.ID;

    }
    public async Task<int> GetBalanceByID(int accountID)
    {
        using var _context = _contextFactory.CreateDbContext();
        try
        {

            Account account = await _context.Accounts.Where(a => a.ID == accountID).FirstAsync();
            return account.Balance;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}

