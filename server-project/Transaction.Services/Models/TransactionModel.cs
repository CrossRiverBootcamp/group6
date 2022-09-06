using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Services.Models
{
    public class TransactionModel
    {
    
        public int FromAccountId { get; set; }
      
        public int ToAccountID { get; set; }
  
        public int Amount { get; set; }
    }
}
