using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAccount.Services.Models
{
    public class LoginResultModel
    {
        public int AccountID { get; set; }
        public string Token { get; set; }
    }
}
