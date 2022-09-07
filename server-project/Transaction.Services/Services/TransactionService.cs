using AutoMapper;
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
        public Task<int> AddTransaction(TransactionModel transactionModel)
        {
            Transaction.Data.Entities.Transaction transaction = _mapper.Map<Transaction.Data.Entities.Transaction>(transactionModel);
            transaction.Status =Status.Processing; 
            transaction.Date = DateTime.UtcNow;
            return _transactionDal.AddTransaction(transaction);
        }

        public Task<bool> UpdateStatusTransaction(StausModel staus)
        {
            if (staus.Success)
             return _transactionDal.UpdateStatus(staus.TransactionID, Status.Success,null);
             return _transactionDal.UpdateStatus(staus.TransactionID, Status.Fail,staus.FailureReason);

        }
    }
}
