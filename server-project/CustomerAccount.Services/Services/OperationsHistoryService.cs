using AutoMapper;
using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomerAccount.Services.Exceptions;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;

namespace CustomerAccount.Services.Services;

public class OperationsHistoryService : IOperationsHistoryService
{
    private IAccountDal _accountDal;
    private IMapper _mapper;
    private readonly IOperationsHistoryDal _operationsHistoryDal;
    public OperationsHistoryService(IAccountDal accountDal, IOperationsHistoryDal operationsHistoryDal)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperModelEntity>();
        });
        _mapper = config.CreateMapper();
        _accountDal = accountDal;
        _operationsHistoryDal = operationsHistoryDal;
    }
    public async Task AddOperationsHistorys(OperationsHistoryToAddModel operationsHistoryToAddModel)
    {
        try
        {
            int balanceTo = await _accountDal.GetBalanceByID(operationsHistoryToAddModel.ToAccountID);
            int balanceFrom = await _accountDal.GetBalanceByID(operationsHistoryToAddModel.FromAccountID);
            OperationsHistory operationsHistoryTO = new OperationsHistory()
            {
                TransactionID = operationsHistoryToAddModel.TransactionID,
                TransactionAmount = operationsHistoryToAddModel.Amount,
                OperationTime = DateTime.UtcNow,
                Balance = balanceTo,
                AccountId = operationsHistoryToAddModel.ToAccountID,
                Credit = true
            };
            OperationsHistory operationsHistoryFrom = new OperationsHistory()
            {
                TransactionID = operationsHistoryToAddModel.TransactionID,
                TransactionAmount = operationsHistoryToAddModel.Amount,
                OperationTime = DateTime.UtcNow,
                Balance = balanceFrom,
                AccountId = operationsHistoryToAddModel.FromAccountID,
                Credit = false
            };
            await _operationsHistoryDal.AddOperationsHistorys(operationsHistoryFrom, operationsHistoryTO);
        }
        catch
        {
            throw new NotSavedException("OperationsHistory wasnt save");
        }

    }
    public async Task<List<OperationsHistoryModel>> GetOperations(int id, int page, int records)
    {
        try
        {
            List<OperationsHistory> ls = await _operationsHistoryDal.GetOperations(id, page, records);
           
            return _mapper.Map<List<OperationsHistory>, List<OperationsHistoryModel>>(ls);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public Task<int> GetNumOfOperations(int accountID)
    {
        return _operationsHistoryDal.GetNumOfOperations(accountID);
    }
}

