using Microsoft.Extensions.Configuration;
using TransactionsAPI.Application.Common.Interfaces;

namespace TransactionsAPI.Infrastructure.Services;

public class HashKeyAccessService : IHashKeyAccessService
{
    private readonly IConfiguration _configuration;

    public HashKeyAccessService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string SecretKey => _configuration.GetValue<string>("HashSecretKey:Key");
}