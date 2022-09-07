using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.NSB
{
    internal class TransferData: ContainSagaData
    {
        public int TransactionID { get; set; }
        //?
        public bool IsTransactionAdded { get; set; }
        public bool IsAmountUpdated { get; set; }
    }
}
