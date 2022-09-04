
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
                Account account = await _contect.Accounts.Include(account=>account.Customer).FirstAsync(c => c.ID == accountID);
                return account;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> Login(string email, string password)
        {
            using var _context = _contextFactory.CreateDbContext();
            try
            {
                return _context.Accounts.Include(a => a.Customer).FirstAsync(c => c.Customer.Email == email && c.Customer.Password == password).Result.ID;
            }
            catch
            {
                return -1;
            }

        }
        public async Task<Customer> GetCustomerByEmail(string email)
        {
            using var _context = _contextFactory.CreateDbContext();
            try
            {
                return  await  _context.Customers.Where(c => c.Email == email).FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }

        }

    }
}
