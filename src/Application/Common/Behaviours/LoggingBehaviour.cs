using TransactionsAPI.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using TransactionsAPI.Application.Transactions.Commands.CreateTransaction;

namespace TransactionsAPI.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;
        string userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }

        if (requestName == "CreateTransactionCommand")
        {
            CreateTransactionCommand cmd = request as CreateTransactionCommand;

            _logger.LogCritical(
                $"Recieved transaction:\n" +
                $" ExternalId={cmd.ExternalTransactionId}\n" +
                $" UserId={cmd.UserId}\n" +
                $" Currency={cmd.Currency}\n" +
                $" Amount={cmd.Amount}");
        }

        _logger.LogInformation("TransactionsAPI Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }

}
