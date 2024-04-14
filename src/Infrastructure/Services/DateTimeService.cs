using TransactionsAPI.Application.Common.Interfaces;

namespace TransactionsAPI.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
