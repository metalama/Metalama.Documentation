// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.Linq;

namespace Doc.LAMA0019.Error;

// Parses the first parameter of the target method into the field or property given by name.
public class ParseIntoAttribute : OverrideMethodAspect
{
    private readonly string _memberName;

    public ParseIntoAttribute( string memberName )
    {
        this._memberName = memberName;
    }

    public override dynamic? OverrideMethod()
    {
        var tryParse = ((INamedType) TypeFactory.GetType( typeof(int) ))
            .Methods.OfName( "TryParse" )
            .Single( m => m.Parameters.Count == 2 && m.Parameters[0].Type.SpecialType == SpecialType.String );

        var member = meta.Target.Type.FieldsAndProperties.OfName( this._memberName ).Single();

        // The second parameter of int.TryParse is an 'out' parameter.
        tryParse.Invoke( meta.Target.Parameters[0].Value, member.Value );

        return meta.Proceed();
    }
}
