﻿

using CustomerAccount.Services.Models;
using Messages.Commands;

namespace CustomerAccount.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountModel> GetAccountInfo(int AccountID);
        
        Task<int> Login(string email, string Password);
        Task<bool> UpdateAccounts(UpdateBalance updateBalanceModel);
    }
}
