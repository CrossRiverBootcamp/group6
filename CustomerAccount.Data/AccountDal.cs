
using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Data
{
    public class AccountDal : IAccountDal
    {
        private IDbContextFactory<CustomerAccountContext> _contextFactory;

        public AccountDal(IDbContextFactory<CustomerAccountContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        
        public bool CreateAccount(Account account, Customer customer)
        {
            using var _contect = _contextFactory.CreateDbContext();
            account.Customer = customer;
            try
            {
                 _contect.Customers.AddAsync(customer);
                 _contect.Accounts.AddAsync(account);
                 _contect.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            

        }

        public async Task<bool> EmailExists(string email)
        {
            using var _contect = _contextFactory.CreateDbContext();
            return await _contect.Customers.AnyAsync(c => c.Email == email);
        }

        public  async Task<Account> GetAccountInfo(int accountID)
        {
            using var _contect = _contextFactory.CreateDbContext();
            try
            {
                //include customer
                return await _contect.Accounts.FirstAsync(c => c.ID == accountID);
            }
            catch
            {
                return null;
            }
        }

        public int Login(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
