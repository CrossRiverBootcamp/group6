using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;

namespace Transaction.WebAPI.NSB.Saga;
internal class TransactionPolicy : Saga<TransferData>, IAmStartedByMessages<TransactionAdded>,
       IHandleMessages<AccountsUpdated>
{
    static ILog log = LogManager.GetLogger<TransactionPolicy>();
    private readonly ITransactionService _transactionService;

    public TransactionPolicy(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransferData> mapper)
    {
        mapper.MapSaga(sagaData => sagaData.TransactionEventID)
       .ToMessage<TransactionAdded>(message => message.TransactionEventID)
       .ToMessage<AccountsUpdated>(message => message.TransactionEventID);
    }
    public async Task Handle(TransactionAdded message, IMessageHandlerContext context)
    {
        log.Info($"received TransactionAdded id: {message.TransactionEventID} from {message.FromAccountID} to {message.ToAccountID}");
        #region addToDB
        TransactionModel transactionModel = new TransactionModel()
        {
            ToAccountID = message.ToAccountID,
            FromAccountId = message.FromAccountID,
            Amount = message.Amount,
        };
        int id = await _transactionService.AddTransactionToDB(transactionModel);
        #endregion 
        //update balance-command;
        UpdateBalance update = new UpdateBalance
        {
            TransactionEventID = message.TransactionEventID,
            TransactionID = id,
            Amount = message.Amount,
            FromAccountID = message.FromAccountID,
            ToAccountID = message.ToAccountID,
        };
        await context.Send(update);
    }

    public async Task Handle(AccountsUpdated message, IMessageHandlerContext context)
    {
        log.Info($"recieved AccountUpdated id:{message.TransactionID} status: {message.Success}");

        StausModel status = new StausModel
        {
            TransactionID = message.TransactionID,
            Success = message.Success,
            FailureReason = message.FailureResult
        };
        await _transactionService.UpdateStatusTransaction(status);
        log.Info($"updateStatus status : {status.Success}");
        MarkAsComplete();
    }

}
