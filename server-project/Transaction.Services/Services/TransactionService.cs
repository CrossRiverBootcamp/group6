using AutoMapper;
using Messages.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Entities;
using Transaction.Data.Interfaces;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;

namespace Transaction.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private ITransactionDal _transactionDal;
        private IMapper _mapper;

        public TransactionService(ITransactionDal transactionDal)
        {
            _transactionDal = transactionDal;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapModelEntity>();
            });
            _mapper = config.CreateMapper();
        }
        public async Task AddTransaction(TransactionModel transactionModel, IMessageSession _session)
        {
            Transaction.Data.Entities.Transaction transaction = _mapper.Map<Transaction.Data.Entities.Transaction>(transactionModel);
            transaction.Status =Status.Processing; 
            transaction.Date = DateTime.UtcNow;
            int id= await _transactionDal.AddTransaction(transaction);
            TransactionAdded transactionEvent = new TransactionAdded
            {
                TransactionID = id,
                FromAccountID = transactionModel.FromAccountId,
                ToAccountID = transactionModel.ToAccountID,
                Amount = transactionModel.Amount

            };
             await  _session.Publish(transactionEvent);
        }

        public Task<bool> UpdateStatusTransaction(StausModel staus)
        {
            if (staus.Success)
             return _transactionDal.UpdateStatus(staus.TransactionID, Status.Success,null);
             return _transactionDal.UpdateStatus(staus.TransactionID, Status.Fail,staus.FailureReason);

        }
    }
}
