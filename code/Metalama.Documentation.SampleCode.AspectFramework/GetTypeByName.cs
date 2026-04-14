// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;

namespace Doc.GetTypeByName;

internal class ResolveTypeAspect : TypeAspect
{
    private static readonly DiagnosticDefinition<string> _info =
        new( "MY001", Severity.Warning, "{0}" );

    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        // Look up a type from the core library by its namespace-qualified metadata name.
        var stringType = TypeFactory.GetType( "System.String" );
        builder.Diagnostics.Report( _info.WithArguments( $"Found: {stringType}" ) );

        // Look up a type from a referenced package (Microsoft.Win32.Registry).
        if ( TypeFactory.TryGetType( "Microsoft.Win32.RegistryKey", out var registryKeyType ) )
        {
            builder.Diagnostics.Report( _info.WithArguments( $"Found: {registryKeyType}" ) );
        }

        // Use '+' for nested types.
        if ( TypeFactory.TryGetType( "System.Environment+SpecialFolder", out var nestedType ) )
        {
            builder.Diagnostics.Report( _info.WithArguments( $"Found nested: {nestedType}" ) );
        }

        // Use backtick notation for generic type definitions.
        if ( TypeFactory.TryGetType( "System.Collections.Generic.Dictionary`2", out var genericType ) )
        {
            builder.Diagnostics.Report( _info.WithArguments( $"Found generic: {genericType}" ) );
        }

        // Returns false for types not referenced by the compilation.
        if ( !TypeFactory.TryGetType( "Some.NonExistent.Type", out _ ) )
        {
            builder.Diagnostics.Report( _info.WithArguments( "Not found: Some.NonExistent.Type" ) );
        }
    }
}

// <target>
[ResolveTypeAspect]
internal class TargetClass;
