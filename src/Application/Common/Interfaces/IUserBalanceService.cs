using TransactionsAPI.Application.Common.Models;

namespace TransactionsAPI.Application.Common.Interfaces;

public interface IUserBalanceService
{
    Task<decimal> GetUserBalance(string userId);
    Task<decimal> AdjustUserBalance(string userId, decimal offset);

    Task<decimal> SetUserBalance(string userId, decimal amount);

}