// This is public domain Metalama sample code.

using Metalama.Extensions.Interceptors;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Engine;
using Metalama.Framework.Engine.AspectWeavers;
using System;
using System.Linq;
using System.Threading.Tasks;

// Both interceptor aspects execute before the weaver of the [Audit] aspect.
[assembly: AspectOrder(
    AspectOrderDirection.RunTime,
    typeof(Doc.LAMA0091.Fixed.AuditAttribute),
    typeof(Doc.LAMA0091.Fixed.InterceptWriteAttribute),
    typeof(Doc.LAMA0091.Fixed.InterceptPrintAttribute) )]

// The weaver of [Audit] must not fall between the layers of the contract aspects of the referenced libraries
// (LAMA0042), so it is ordered after all of them.
[assembly: AspectOrder( AspectOrderDirection.RunTime, typeof(Doc.LAMA0091.Fixed.AuditAttribute), typeof(ContractAspect) )]

namespace Doc.LAMA0091.Fixed;

// A low-level aspect, which requires a low-level aspect weaver.
[RequireAspectWeaver( "Doc.LAMA0091.Fixed.AuditWeaver" )]
internal class AuditAttribute : TypeAspect { }

[MetalamaPlugIn]
internal class AuditWeaver : IAspectWeaver
{
    public Task TransformAsync( AspectWeaverContext context ) => Task.CompletedTask;
}

// This aspect registers an interceptor for the Print method.
internal class InterceptPrintAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            b => b
                .Type( typeof(Target) ).Methods( "Print" )
                .RedirectToExistingMethod( typeof(Redirections).AsINamedType().Methods.OfName( "Print" ).Single() ) );
}

// This aspect registers an interceptor for the Write method.
internal class InterceptWriteAttribute : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
        => builder.InterceptMethods(
            b => b
                .Type( typeof(Target) ).Methods( "Write" )
                .RedirectToExistingMethod( typeof(Redirections).AsINamedType().Methods.OfName( "Write" ).Single() ) );
}

internal static class Target
{
    public static void Print( string value ) => Console.WriteLine( "target: " + value );

    public static void Write( string value ) => Console.WriteLine( "target: " + value );
}

internal static class Redirections
{
    public static void Print( string value ) => Console.WriteLine( "intercepted: " + value );

    public static void Write( string value ) => Console.WriteLine( "intercepted: " + value );
}
