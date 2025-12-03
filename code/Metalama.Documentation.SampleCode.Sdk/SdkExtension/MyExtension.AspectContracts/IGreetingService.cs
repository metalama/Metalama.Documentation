// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Services;

[assembly: CompileTime]

namespace MyExtension.AspectContracts;

public interface IGreetingService : IProjectService
{
    string GetGreeting( string name );
}
