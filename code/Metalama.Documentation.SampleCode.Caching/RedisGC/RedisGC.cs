// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.Redis;
using Metalama.Patterns.Caching.Backends.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

#if TEST_OPTIONS
// @DisableCompareProgramOutput
#endif

namespace Doc.RedisGC;

public sealed class Program
{
    public static async Task Main( string[] args )
    {
        var appBuilder = Host.CreateApplicationBuilder();

        // Add a local Redis server with a random-assigned port. You don't need this in your code.
        using var redis = appBuilder.Services.AddLocalRedisServer();
        var endpoint = redis.Endpoint;

        // Add the garbage collected service, implemented as IHostedService.
        appBuilder.Services.AddRedisCacheDependencyGarbageCollector( _ =>
        {
            // Build the Redis connection options.
            var redisConnectionOptions = new ConfigurationOptions();
            redisConnectionOptions.EndPoints.Add( endpoint.Address, endpoint.Port );

            // The KeyPrefix must match _exactly_ the one used by the caching back-end.
            var keyPrefix = "TheApp.1.0.0";

            return new RedisCachingBackendConfiguration
            {
                NewConnectionOptions = redisConnectionOptions, KeyPrefix = keyPrefix
            };
        } );

        var host = appBuilder.Build();

        await host.StartAsync();

        if ( args.Contains( "--full" ) )
        {
            Console.WriteLine( "Performing full collection." );

            var collector =
                host.Services.GetRequiredService<RedisCacheDependencyGarbageCollector>();

            await collector.PerformFullCollectionAsync();
            Console.WriteLine( "Full collection completed." );
        }

        const bool isProductionCode = false;

        if ( isProductionCode )
        {
            // await host.WaitForShutdownAsync();
        }
        else
        {
            // This code is automatically executed so we shut down the service after 1 second.
            await Task.Delay( 1000 );
            await host.StopAsync();
        }
    }
}