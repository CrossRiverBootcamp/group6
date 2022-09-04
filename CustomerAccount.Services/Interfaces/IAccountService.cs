

using CustomerAccount.Services.Models;

namespace CustomerAccount.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountModel> GetAccountInfo(int AccountID);
        Task<int> Login(string email, string Password);
    }
}
