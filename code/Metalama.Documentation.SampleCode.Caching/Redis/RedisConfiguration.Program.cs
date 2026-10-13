// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Documentation.Helpers.Redis;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Backends.Redis;
using Metalama.Patterns.Caching.Building;
using Metalama.Patterns.Caching.Resilience;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Doc.RedisConfiguration;

internal static class Program
{
    public static async Task Main()
    {
        var builder = ConsoleApp.CreateBuilder();

        // Add a local Redis server with a random-assigned port. You don't need this in your code.
        using var redis = builder.Services.AddLocalRedisServer();
        var endpoint = redis.Endpoint;

        builder.Services.AddSingleton<IConnectionMultiplexer>( _ =>
        {
            var redisConnectionOptions = new ConfigurationOptions();
            redisConnectionOptions.EndPoints.Add( endpoint.Address, endpoint.Port );

            return ConnectionMultiplexer.Connect( redisConnectionOptions );
        } );

        // [<snippet ConfigureResilience>]
        builder.Services.AddMetalamaCaching(
            caching => caching.WithBackend(
                backend => backend.Redis(
                    new RedisCachingBackendConfiguration
                    {
                        // Set a custom transaction retry policy with more attempts.
                        TransactionRetryPolicy = new RetryPolicy
                        {
                            MaxAttempts = 10,
                            BaseDelay = TimeSpan.FromMilliseconds( 50 ),
                            MaxDelay = TimeSpan.FromSeconds( 5 )
                        },

                        // Set the concurrency limits for background operations.
                        BackgroundTasksMaxConcurrency = 50,
                        BackgroundTasksOverloadedThreshold = 200,

                        // Enable key compression for keys longer than 256 characters.
                        KeyCompressingThreshold = 256
                    } ) ) );

        // [<endsnippet ConfigureResilience>]

        builder.Services.AddAsyncConsoleMain<ConsoleMain>();
        builder.Services.AddSingleton<CloudCalculator>();

        await using var app = builder.Build();

        await app.Services.GetRequiredService<ICachingService>().InitializeAsync();

        await app.RunAsync();
    }
}
