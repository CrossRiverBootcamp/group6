using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
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
            return true;
        }
        catch 
        { 
            throw;
        }

    }

    public async Task<bool> EmailExists(string email)
    {
        using var _contect = _contextFactory.CreateDbContext();
        return await _contect.Customers.AnyAsync(c => c.Email == email);
    }



    public async Task<Account> GetAccountInfo(int accountID)
    {
        using var _contect = _contextFactory.CreateDbContext();
        try
        {
            //include customer
            Account account = await _contect.Accounts.Include(account => account.Customer).FirstAsync(c => c.ID == accountID);
            return account;
        }
        catch
        {
            throw;
        }
    }


    public async Task<Account> FindUpdateAccount(int ID)
    {
        using var _contect = _contextFactory.CreateDbContext();
        Account acccountToUpDate = await _contect.Accounts.Where(a => a.ID ==ID).FirstOrDefaultAsync();
        if (acccountToUpDate == null)
        {
            return null;
        }
        return acccountToUpDate;
        
    }
    public async Task<string> UpdateAccounts(Account accountFrom,Account accountTo)
    {
        using var _contect = _contextFactory.CreateDbContext();
        try
        { 
            if(accountFrom==null|| accountTo == null)
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
    public async Task<int> Login(string email, string password)
    {
        using var _context = _contextFactory.CreateDbContext();
        try
        {
            Account account = await _context.Accounts.Include(a => a.Customer).FirstAsync(c => c.Customer.Email == email && c.Customer.Password == password);
            return account.ID;
        }
        catch
        {
            throw;
        }

    }
    public async Task<Customer> GetCustomerByEmail(string email)
    {
        using var _context = _contextFactory.CreateDbContext();
        try
        {
            return await _context.Customers.Where(c => c.Email == email).FirstOrDefaultAsync();
        }
        catch
        {
            throw;
        }

    }
    public async Task<int> GetBalanceByID(int accountID)
    {
        using var _context = _contextFactory.CreateDbContext();
        try
        {
            Account account = await _context.Accounts.Where(a => a.ID == accountID).FirstAsync();
            return account.Balance;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }


}

