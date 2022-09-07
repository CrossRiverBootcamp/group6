using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Services.Models;

namespace Transaction.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<int> AddTransaction(TransactionModel transactionModel);
        Task<bool> UpdateStatusTransaction(StausModel staus);
    }
}
