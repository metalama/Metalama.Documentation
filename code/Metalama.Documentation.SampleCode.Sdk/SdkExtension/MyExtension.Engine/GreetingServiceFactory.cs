// This is public domain Metalama sample code.

using Metalama.Framework.Engine.Extensibility;
using Metalama.Framework.Engine.Services;
using Metalama.Framework.Services;
using System.Collections.Generic;

[assembly: ExportExtension( typeof( MyExtension.Engine.GreetingServiceFactory ), ExtensionKinds.ServiceFactory )]

namespace MyExtension.Engine;

public class GreetingServiceFactory : IProjectServiceFactory
{
    public IEnumerable<IProjectService> CreateServices( in ProjectServiceProvider serviceProvider )
    {
        return new IProjectService[] { new GreetingService() };
    }
}
