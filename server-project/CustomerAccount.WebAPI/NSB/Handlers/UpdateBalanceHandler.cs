using AutoMapper;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace CustomerAccount.WebAPI.NSB.Handlers;
public class UpdateBalanceHandler : IHandleMessages<UpdateBalance>
{
    static ILog log = LogManager.GetLogger<UpdateBalanceHandler>();
   private readonly IAccountService _accountService;
    private IMapper _mapper;
    private readonly IOperationsHistoryService _operationsHistoryService;

    public UpdateBalanceHandler(IAccountService accountService, IOperationsHistoryService operationsHistoryService)
    {
        _accountService = accountService;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperDtoModels>();
        });
        _mapper = config.CreateMapper();
        _operationsHistoryService = operationsHistoryService;
    }
   
    


    public async Task Handle(UpdateBalance message, IMessageHandlerContext context)
    {

        log.Info($"Received UpdateBalance, TransactionID = {message.TransactionID}");
        bool success = false; ;
        string reason = await _accountService.UpdateAccounts(message);
        if (reason == null)
        {
            success = true;
        }
        if (success)
        {
            //OperationsHistoryToAddModel operationsHistoryToAddModel = _mapper.Map<OperationsHistoryToAddModel>(message);
            OperationsHistoryToAddModel operationsHistoryToAdd = new OperationsHistoryToAddModel()
            {
                Amount = message.Amount,
                ToAccountID = message.ToAccountID,
                FromAccountID = message.FromAccountID,
                TransactionID=message.TransactionID,
            };
            await _operationsHistoryService.AddOperationsHistorys(operationsHistoryToAdd);

        }
        AccountsUpdated accountsUpdated = new AccountsUpdated()
        {
            Success = success,
            FailureResult = reason,
            TransactionID = message.TransactionID,
            TransactionEventID = message.TransactionEventID,
        };
        await context.Publish(accountsUpdated);

    }
}

