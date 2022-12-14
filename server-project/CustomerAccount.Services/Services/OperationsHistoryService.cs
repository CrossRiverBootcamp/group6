using AutoMapper;
using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
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
    public async Task<List<OperationsHistoryModel>> GetOperations(int accountID, int page, int records)
    {
        List<OperationsHistory> operationsHistories = await _operationsHistoryDal.GetOperations(accountID, page, records);
        return _mapper.Map<List<OperationsHistory>, List<OperationsHistoryModel>>(operationsHistories);
    }
    public Task<int> GetNumOfOperations(int accountID)
    {
        return _operationsHistoryDal.GetNumOfOperations(accountID);
    }
}

