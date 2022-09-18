using CustomerAccount.Services.Interfaces;
using Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;

namespace CustomerAccount.WebAPI.NSB.Handlers;

public class DeleteRowsExpiredHandler : IHandleMessages<DeleteExpiredCodes>
{
    static ILog _log = LogManager.GetLogger<DeleteRowsExpiredHandler>();
    private IEmailVerificationService _emailVerificationService;

    public DeleteRowsExpiredHandler(IEmailVerificationService emailVerificationService)
    {
        _emailVerificationService = emailVerificationService;
    }
    public async Task Handle(DeleteExpiredCodes message, IMessageHandlerContext context)
    {
        _log.Info($"recieved ${typeof(DeleteExpiredCodes)} on ${DateTime.UtcNow}");
        int numRowsEffected = await _emailVerificationService.DeleteExpiredCodes();
        if (numRowsEffected > 0)
        {
            _log.Info($"{numRowsEffected} rows is deleted");
        }
    }
}

