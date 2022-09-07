using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Entities;

namespace Transaction.Data.Interfaces
{
    public interface ITransactionDal
    {
        Task<int> AddTransaction(Transaction.Data.Entities.Transaction transaction);
        Task<bool> UpdateStatus(int transactionID,Status status,string? failureReason);
/*        Task<Transaction.Data.Entities.Transaction> GetTransaction(int transactionID);
*/    }
}
