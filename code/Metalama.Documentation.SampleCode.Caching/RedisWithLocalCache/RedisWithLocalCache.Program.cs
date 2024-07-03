﻿// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Documentation.Helpers.Redis;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Backends.Redis;
using Metalama.Patterns.Caching.Building;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Doc.RedisWithLocalCache;

internal static class Program
{
    public static async Task Main()
    {
        var builder = ConsoleApp.CreateBuilder();

        // Add a local Redis server with a random-assigned port. You don't need this in your code.
        using var redis = builder.Services.AddLocalRedisServer();
        var endpoint = redis.Endpoint;

        // Add Redis.                                                           /*<AddRedis>*/
        builder.Services.AddSingleton<IConnectionMultiplexer>(
            _ =>
            {
                // Build the Redis connection options.
                var redisConnectionOptions = new ConfigurationOptions();
                redisConnectionOptions.EndPoints.Add( endpoint.Address, endpoint.Port );

                return ConnectionMultiplexer.Connect( redisConnectionOptions );
            } ); /*</AddRedis>*/

        // Add the caching service.                         
        builder.Services.AddMetalamaCaching(                                         /*<AddMetalamaCaching>*/
            caching => caching.WithBackend( backend => backend.Redis().WithL1() ) ); /*</AddMetalamaCaching>*/

        // Add other components as usual.
        builder.Services.AddAsyncConsoleMain<ConsoleMain>();
        builder.Services.AddSingleton<CloudCalculator>();

        // Build the host.
        await using var app = builder.Build();

        // Initialize caching.
        await app.Services.GetRequiredService<ICachingService>().InitializeAsync(); /*<Initialize>*/
        /*</Initialize>*/

        // Run the host.
        await app.RunAsync();
    }
}