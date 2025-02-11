// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture;
using Metalama.Extensions.Architecture.Predicates;
using Metalama.Extensions.Validation;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;

namespace Doc.Architecture.Fabric_CustomPredicate;

// This class is the actual implementation of the predicate.
internal class MethodNamePredicate : ReferenceEndPredicate
{
    private readonly string _suffix;

    public MethodNamePredicate( ReferencePredicateBuilder builder, string suffix ) : base( builder )
    {
        this._suffix = suffix;
    }

    protected override ReferenceGranularity GetGranularity() => ReferenceGranularity.Member;

    public override bool IsMatch( ReferenceEnd referenceEnd )
        => referenceEnd.Member is IMethod method && method.Name.EndsWith(
            this._suffix,
            StringComparison.Ordinal );
}

// This class exposes the predicate as an extension method. It is your public API.
[CompileTime]
public static class Extensions
{
    public static ReferencePredicate MethodNameEndsWith(
        this ReferencePredicateBuilder builder,
        string suffix )
        => new MethodNamePredicate( builder, suffix );
}

// Here is how your new predicate can be used.
internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.SelectReflectionType( typeof(CofeeMachine) )
            .CanOnlyBeUsedFrom( r => r.MethodNameEndsWith( "Politely" ) );
    }
}

// This is the class whose access are validated.
internal static class CofeeMachine
{
    public static void TurnOn() { }
}

internal class Bar
{
    public static void OrderCoffee()
    {
        // Forbidden because the method name does not end with Politely.
        CofeeMachine.TurnOn();
    }

    public static void OrderCoffeePolitely()
    {
        // Allowed.
        CofeeMachine.TurnOn();
    }
}