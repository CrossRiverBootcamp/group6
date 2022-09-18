using CustomerAccount.Services.Models;

namespace CustomerAccount.Services.Interfaces
{
    public interface IOperationsHistoryService
    {
        Task AddOperationsHistorys(OperationsHistoryToAddModel operationsHistoryToAddModel);
        Task<List<OperationsHistoryModel>> GetOperations(int accountID, int page, int records);
        public Task<int> GetNumOfOperations(int accountID);
    }
}