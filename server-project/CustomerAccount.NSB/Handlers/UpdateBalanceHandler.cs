using AutoMapper;
using CustomerAccount.Services;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Models;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;


namespace CustomerAccount.NSB.Handlers;

public class UpdateBalanceHandler : IHandleMessages<UpdateBalance>
{
    static ILog log = LogManager.GetLogger<UpdateBalanceHandler>();
    IAccountService _accountService;
    public UpdateBalanceHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }


    public async Task Handle(UpdateBalance message, IMessageHandlerContext context)
    {

        log.Info($"Received UpdateBalance, TransactionID = {message.TransactionID}");
        bool success = false; ;
        string reason = await _accountService.UpdateAccounts(message);
        if (reason==null) {
            success = true;
        }
        AccountsUpdated accountsUpdated = new AccountsUpdated()
        {
            Success = success,
            FailureResult = reason,
            TransactionID = message.TransactionID,
        };
        await context.Publish(accountsUpdated); 

    }
}
