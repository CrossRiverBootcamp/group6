using CustomerAccount.Data.Entities;


namespace CustomerAccount.Data.Interfaces
{
    public interface IAccountDal
    {
        bool CreateAccount(Account account, Customer customer);
        int Login(string email,string password);
        Task<Account> GetAccountInfo(int accountID);
        Task<bool> EmailExists(string email);


    }
}
