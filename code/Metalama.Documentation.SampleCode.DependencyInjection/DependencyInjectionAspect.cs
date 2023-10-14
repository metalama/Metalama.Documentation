﻿// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

#pragma warning disable CS0169 // Field is never used
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace Doc.DependencyInjectionAspect
{
    public class DependencyInjectionAspect
    {
        // This dependency will be optional because the field is nullable.
        [Dependency]
        private ILogger? _logger;

        // This dependency will be required because the field is non-nullable.
        [Dependency]
        private IHostEnvironment _environment;
        
        // This dependency will be retrieved on demand.
        [Dependency( IsLazy = true )]
        private IHostApplicationLifetime _lifetime;

        public void DoWork()
        {
            this._logger?.LogDebug( "Doing some work." );

            if ( !this._environment.IsProduction() )
            {
                this._lifetime.StopApplication();
            }
        }
    }
}