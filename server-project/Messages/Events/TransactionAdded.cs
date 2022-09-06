using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Events
{
    public  class TransactionAdded
    {
        public int TransactionID { get; set; }
        public int FromAccountID { get; set; }
        public int ToAccountID { get; set; }
        public int Amount { get; set; }
    }
}
