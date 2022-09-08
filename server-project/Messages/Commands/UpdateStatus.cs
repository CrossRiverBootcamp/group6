using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Commands
{
    public class UpdateStatus
    {
        public int TransactionID { get; set; }
        public bool Success { get; set; }
        public string? FailureReason { get; set; }
    }
}
