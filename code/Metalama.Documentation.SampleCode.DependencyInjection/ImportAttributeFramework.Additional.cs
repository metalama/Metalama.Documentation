// This is public domain Metalama sample code.

using System;

namespace Doc.ImportAttributeFramework;

// The [Import] attribute used by an external DI container.
[AttributeUsage( AttributeTargets.Property )]
public class ImportAttribute : Attribute { }

// An interface that the external DI container will resolve.
public interface ILogger
{
    void Log( string message );
}
