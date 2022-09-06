using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data.Interfaces
{
    public interface ITransactionDal
    {
        Task<int> AddTransaction(Transaction.Data.Entities.Transaction transaction);
    }
}
