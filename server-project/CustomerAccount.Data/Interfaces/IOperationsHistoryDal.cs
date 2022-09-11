using CustomerAccount.Data.Entities;

namespace CustomerAccount.Data.Interfaces;

    public interface IOperationsHistoryDal
    {
        Task AddOperationsHistorys(OperationsHistory operationsHistoryFrom, OperationsHistory operationsHistoryTo);
        Task<List<OperationsHistory>> GetOperations(int id, int page, int records);
    }
