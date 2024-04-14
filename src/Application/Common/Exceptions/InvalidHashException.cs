namespace TransactionsAPI.Application.Common.Exceptions;

public class InvalidHashException : Exception
{
    public InvalidHashException()
        : base("Hash values not matching")
    {
    }
}