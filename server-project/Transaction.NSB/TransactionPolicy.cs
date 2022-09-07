using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;

namespace Transaction.NSB
{
    internal class TransactionPolicy : Saga<TransferData>, IAmStartedByMessages<TransactionAdded>,
        IHandleMessages<AccountsUpdated>
    {
        static ILog log = LogManager.GetLogger<TransactionPolicy>();
        private ITransactionService _transactionService;

        public TransactionPolicy(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransferData> mapper)
        {
            mapper.MapSaga(sagaData => sagaData.TransactionID)
        .ToMessage<TransactionAdded>(message => message.TransactionID)
        .ToMessage<AccountsUpdated>(message => message.TransactionID);
        }
        public Task Handle(TransactionAdded message, IMessageHandlerContext context)
        {
            log.Info($"received TransactionAdded id: {message.TransactionID} from {message.FromAccountID} to {message.ToAccountID}");
            #region
            //?-this message start the saga
            //evansully its handled
            //otherwise the saga wouldnt even start...
            Data.IsTransactionAdded = true;
            #endregion
            //update balance-command;
            UpdateBalance update = new UpdateBalance
            {
                TransactionID = message.TransactionID,
                Amount = message.Amount,
                FromAccountID = message.FromAccountID,
                ToAccountID = message.ToAccountID,
            };
          return context.Send(update);
        }

        public async Task Handle(AccountsUpdated message, IMessageHandlerContext context)
        {
            log.Info($"recieved AccountUpdated id:{message.TransactionID} status: {message.Success}");
            Data.IsAmountUpdated = true;
            StausModel status = new StausModel
            {
                 TransactionID=message.TransactionID,
                 Success=message.Success,
                 FailureReason=message.FailureResult
            };
            bool successUpdateStatus =await _transactionService.UpdateStatusTransaction(status);
            log.Info($"updateStatus status : {status.Success}");
            MarkAsComplete();
        }

    }
}
