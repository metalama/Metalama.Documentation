// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;

namespace Doc.GlobalImport;

internal class ImportAttribute : OverrideFieldOrPropertyAspect
{
    public override dynamic? OverrideProperty
    {
        get
            => ServiceLocator.ServiceProvider.GetService(
                meta.Target.FieldOrProperty.Type.ToType() );

        set
            => throw new NotSupportedException(
                $"{meta.Target.FieldOrProperty.Name} should not be set from source code." );
    }
}