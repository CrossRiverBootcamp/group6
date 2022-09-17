using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class NsbNotPublishedException : Exception
    {
        public NsbNotPublishedException(string? message) : base(message)
        {
        }
    }
}
