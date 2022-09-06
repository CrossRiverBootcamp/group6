using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Events
{
    public class AccountsUpdated
    {
        public int TransactionID { get; set; }
        public bool Success { get; set; }
        public string? FailureResult { get; set; }
    }
}
