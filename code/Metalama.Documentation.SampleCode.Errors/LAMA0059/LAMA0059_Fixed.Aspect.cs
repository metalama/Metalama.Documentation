// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System;

namespace Doc.LAMA0059.Fixed;

// Fixed: the aspect class isn't generic; the type is passed as a constructor argument.
public class IdAttribute : TypeAspect
{
    private readonly Type _idType;

    public IdAttribute( Type idType )
    {
        this._idType = idType;
    }

    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        builder.IntroduceAutomaticProperty(
            "Id",
            TypeFactory.GetType( this._idType ),
            buildProperty: p => p.Accessibility = Accessibility.Public );
    }
}
