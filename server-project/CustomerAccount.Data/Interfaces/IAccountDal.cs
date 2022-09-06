using CustomerAccount.Data.Entities;


namespace CustomerAccount.Data.Interfaces
{
    public interface IAccountDal
    {
        Task<bool> CreateAccount(Account account, Customer customer);
        Task<int> Login(string email,string password);
        Task<Account> GetAccountInfo(int accountID);
        Task<bool> EmailExists(string email);
        Task<Customer> GetCustomerByEmail(string email);

    }
}
