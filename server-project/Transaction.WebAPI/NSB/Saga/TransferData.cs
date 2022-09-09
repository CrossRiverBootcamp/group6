using NServiceBus;

namespace Transaction.WebAPI.NSB.Saga;
 public class TransferData : ContainSagaData
{
    public int TransactionID { get; set; }
}
