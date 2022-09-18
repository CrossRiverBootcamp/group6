using AutoMapper;
using CustomExceptions;
using Messages.Events;
using NServiceBus;
using Transaction.Data.Entities;
using Transaction.Data.Interfaces;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;

namespace Transaction.Services.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionDal _transactionDal;
    private readonly IMapper _mapper;

    public TransactionService(ITransactionDal transactionDal)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapModelEntity>();
        });
        _mapper = config.CreateMapper();
        _transactionDal = transactionDal;
    }
    public async Task<bool> AddTransaction(TransactionModel transactionModel, IMessageSession session)
    {
        TransactionAdded transactionEvent = new TransactionAdded
        {
            TransactionEventID = Guid.NewGuid(),
            FromAccountID = transactionModel.FromAccountId,
            ToAccountID = transactionModel.ToAccountID,
            Amount = transactionModel.Amount
        };
        try
        {
            await session.Publish(transactionEvent);
            return true;
        }
        catch
        {
            throw new NsbNotPublishedException($"transaction not created from: {transactionEvent.FromAccountID} to: {transactionEvent.ToAccountID} of- {transactionEvent.Amount}");
        }

    }

    public Task<int> AddTransactionToDB(TransactionModel transactionModel)
    {
        Data.Entities.Transaction transaction = _mapper.Map<Data.Entities.Transaction>(transactionModel);
        transaction.Status = Status.Processing;
        transaction.Date = DateTime.UtcNow;
        return _transactionDal.AddTransaction(transaction);
    }
    public async Task UpdateStatusTransaction(StausModel status)
    {
        if (status.Success)
        {
            await _transactionDal.UpdateStatus(status.TransactionID, Status.Success, null);
        }
        await _transactionDal.UpdateStatus(status.TransactionID, Status.Fail, status.FailureReason);

    }
}