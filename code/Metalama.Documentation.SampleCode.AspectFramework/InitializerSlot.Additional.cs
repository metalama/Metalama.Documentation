// This is public domain Metalama sample code.

using Metalama.Framework.RunTime.Initialization;
using System;

namespace Doc.InitializerSlot;

// Slots live on a plain (non-[CompileTime]) static holder because aspect types are
// [CompileTime], so their static fields cannot flow into run-time template code.
public static class InitializerSlots
{
    public static readonly InitializationSlot Validate = InitializationSlot.Allocate();

    public static readonly InitializationSlot Publish = InitializationSlot.Allocate();
}

public static class ValidationService
{
    public static void Validate( object entity )
        => Console.WriteLine( $"Validated {entity.GetType().Name}" );
}

public static class PublishService
{
    public static void Publish( object entity )
        => Console.WriteLine( $"Published {entity.GetType().Name}" );
}
