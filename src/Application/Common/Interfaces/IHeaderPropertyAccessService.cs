namespace TransactionsAPI.Application.Common.Interfaces;

public interface IHeaderPropertyAccessService
{
    string? GetPropertyValue(string propertyName);
}
