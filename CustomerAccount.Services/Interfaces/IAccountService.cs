

using CustomerAccount.Services.Models;

namespace CustomerAccount.Services.Interfaces
{
    public interface IAccountService
    {
        AccountModel GetAccountInfo(int AccountID);
       // int Login(string UserName, string Password);
    }
}
