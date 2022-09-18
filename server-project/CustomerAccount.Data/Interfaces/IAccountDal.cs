using CustomerAccount.Data.Entities;

namespace CustomerAccount.Data.Interfaces;

public interface IAccountDal
{
    Task<bool> CreateAccount(Account account, Customer customer);
    Task<int?> Login(string email, string password);
    Task<Account?> GetAccountInfo(int accountID);
    Task<bool> EmailExists(string email);
    Task<string> GetSaltByEmail(string email);
    Task UpdateAccounts(Account accountFrom, Account accountTo);
    Task<Account?> FindUpdateAccount(int ID);
    Task<int> GetBalanceByID(int accountID);

}

