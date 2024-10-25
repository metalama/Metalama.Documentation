// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.IO;
using System.Linq;

namespace Doc.DynamicCodeModel;

internal class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var loggerField = meta.Target.Type.FieldsAndProperties
            .Single( x => x.Type.IsConvertibleTo( typeof(TextWriter) ) );

        ((TextWriter) loggerField.Value!).WriteLine( $"Executing {meta.Target.Method}." );

        return meta.Proceed();
    }
}