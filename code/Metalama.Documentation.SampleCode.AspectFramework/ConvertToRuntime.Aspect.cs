// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System;
using System.Linq;

namespace Doc.ConvertToRunTime;

internal class ConvertToRunTimeAspect : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var parameterNamesCompileTime = meta.Target.Parameters.Select( p => p.Name ).ToList();
        var parameterNames = meta.RunTime( parameterNamesCompileTime );

        var buildTime = meta.RunTime(
            meta.CompileTime( new Guid( "13c139ea-42f5-4726-894d-550406357978" ) ) );

        var parameterType = meta.RunTime( meta.Target.Parameters[0].Type.ToType() );

        return null;
    }
}