// This is public domain Metalama sample code.

using MyExtension.AspectContracts;

namespace MyExtension.Engine;

internal class GreetingService : IGreetingService
{
    public string GetGreeting( string name ) => $"Hello, {name}!";
}
