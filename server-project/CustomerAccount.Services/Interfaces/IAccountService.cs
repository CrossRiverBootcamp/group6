using CustomerAccount.Services.Models;
using Messages.Commands;

namespace CustomerAccount.Services.Interfaces;
public interface IAccountService
{
    Task<AccountModel> GetAccountInfo(int AccountID);  
    Task<LoginResultModel> Login(string email, string Password);
    Task<string> UpdateAccounts(UpdateBalance updateBalanceModel);
}
