using AutoMapper;
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
            return false;
        }

    }

    public async Task<int> AddTransactionToDB(TransactionModel transactionModel)
    {
        Data.Entities.Transaction transaction = _mapper.Map<Data.Entities.Transaction>(transactionModel);
        transaction.Status = Status.Processing;
        transaction.Date = DateTime.UtcNow;
        int id = await _transactionDal.AddTransaction(transaction);
        return id;

    }
    public Task<bool> UpdateStatusTransaction(StausModel status)
    {
        if (status.Success)
        {
            return _transactionDal.UpdateStatus(status.TransactionID, Status.Success, null);
        }
        return _transactionDal.UpdateStatus(status.TransactionID, Status.Fail, status.FailureReason);

    }
}