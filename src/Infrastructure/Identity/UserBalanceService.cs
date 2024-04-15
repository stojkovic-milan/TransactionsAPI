using TransactionsAPI.Application.Common.Interfaces;
using TransactionsAPI.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TransactionsAPI.Infrastructure.Persistence;

namespace TransactionsAPI.Infrastructure.Identity;

public class UserBalanceService : IUserBalanceService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public UserBalanceService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<decimal> GetUserBalance(string userId)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
        return user.Balance;
    }

    public async Task<decimal> AdjustUserBalance(string userId, decimal offset)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
        user.Balance += offset;
        await _userManager.UpdateAsync(user);
        await _context.SaveChangesAsync();
        return user.Balance;
    }

    public async Task<decimal> SetUserBalance(string userId, decimal amount)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
        user.Balance = amount;
        await _userManager.UpdateAsync(user);
        await _context.SaveChangesAsync();
        return user.Balance;
    }
}