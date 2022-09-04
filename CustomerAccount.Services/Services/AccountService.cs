using CustomerAccount.Data.Interfaces;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.Services.Services
{
    public class AccountService : IAccountService
    {
        private IAccountDal _accountDal;

        public AccountService(IAccountDal accountDal)
        {
            _accountDal = accountDal;
        }
        public AccountModel GetAccountInfo(int AccountID)
        {
            throw new NotImplementedException();
        }
    }
}
