// This is public domain Metalama sample code.

using Microsoft.Extensions.DependencyInjection;

namespace Metalama.Documentation.Helpers.Redis;

public static class LocalRedisServerFactory
{
    public static LocalRedisServer AddLocalRedisServer( this IServiceCollection serviceCollection )
    {
        var server = new LocalRedisServer();
        serviceCollection.AddSingleton( server );

        return server;
    }
}