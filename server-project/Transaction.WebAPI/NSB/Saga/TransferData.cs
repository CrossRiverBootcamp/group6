using NServiceBus;

namespace Transaction.WebAPI.NSB.Saga;
 public class TransferData : ContainSagaData
{
    public Guid TransactionEventID { get; set; }
}
