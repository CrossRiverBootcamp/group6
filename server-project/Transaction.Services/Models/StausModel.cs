using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Services.Models
{
    public class StausModel
    {
        public int TransactionID { get; set; }
        public bool Success { get; set; }
        public string? FailureReason { get; set; }

    }
}
