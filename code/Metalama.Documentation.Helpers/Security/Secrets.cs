// This is public domain Metalama sample code.

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Collections.Concurrent;

namespace Metalama.Documentation.Helpers.Security;

public static class Secrets
{
    static Secrets()
    {
        var defaultAzureCredentialOptions = new DefaultAzureCredentialOptions()
        {
            TenantId = Environment.GetEnvironmentVariable( "AZURE_TENANT_ID" )
                       ?? throw new InvalidOperationException(
                           "The AZURE_TENANT_ID environment variable must be defined for this test." )
        };

        _client = new SecretClient(
            new Uri( "https://testserviceskeyvault.vault.azure.net/" ),
            new DefaultAzureCredential( defaultAzureCredentialOptions ) );
    }

    private static readonly SecretClient _client;

    private static readonly ConcurrentDictionary<string, string> _secrets = new();

    public static string Get( string name )
        => _secrets.GetOrAdd( name, n => _client.GetSecret( n ).Value.Value );
}