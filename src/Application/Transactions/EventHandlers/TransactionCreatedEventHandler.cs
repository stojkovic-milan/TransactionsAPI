using CleanTemplate.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using TransactionsAPI.Application.Common.Interfaces;

namespace TransactionsAPI.Application.Transactions.EventHandlers;

public class TransactionCreatedEventHandler : INotificationHandler<TransactionCreatedEvent>
{
    private readonly ILogger<TransactionCreatedEventHandler> _logger;
    private readonly IUserBalanceService _userBalanceService;
    public TransactionCreatedEventHandler(ILogger<TransactionCreatedEventHandler> logger, IApplicationDbContext context, IIdentityService identityService, IUserBalanceService userBalanceService)
    {
        _logger = logger;
        _userBalanceService = userBalanceService;
    }

    public Task Handle(TransactionCreatedEvent notification, CancellationToken cancellationToken)
    {
        _userBalanceService.AdjustUserBalance(notification.Item.UserId, notification.Item.Amount);
        _logger.LogInformation("CleanTemplate Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
