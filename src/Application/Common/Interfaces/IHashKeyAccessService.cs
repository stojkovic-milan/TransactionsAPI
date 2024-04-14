namespace TransactionsAPI.Application.Common.Interfaces;

public interface IHashKeyAccessService
{
    string? GetHashKey(string propertyName);
}
