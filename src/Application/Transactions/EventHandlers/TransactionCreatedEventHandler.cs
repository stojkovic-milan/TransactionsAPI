using CleanTemplate.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using TransactionsAPI.Application.Common.Interfaces;

namespace TransactionsAPI.Application.Transactions.EventHandlers;

public class TransactionCreatedEventHandler : INotificationHandler<TransactionCreatedEvent>
{
    private readonly ILogger<TransactionCreatedEventHandler> _logger;
    private readonly IUserBalanceService _userBalanceService;

    public TransactionCreatedEventHandler(ILogger<TransactionCreatedEventHandler> logger, IApplicationDbContext context,
        IIdentityService identityService, IUserBalanceService userBalanceService)
    {
        _logger = logger;
        _userBalanceService = userBalanceService;
    }

    public Task Handle(TransactionCreatedEvent notification, CancellationToken cancellationToken)
    {
        _userBalanceService.AdjustUserBalance(notification.Item.UserId, notification.Item.Amount);
        _logger.LogCritical(
            $"Recieved transaction:\n" +
            $" ExternalId={notification.Item.ExternalTransactionId}\n" +
            $" UserId={notification.Item.UserId}\n" +
            $" Currency={notification.Item.Currency}\n" +
            $" Amount={notification.Item.Amount}");
        return Task.CompletedTask;
    }
}