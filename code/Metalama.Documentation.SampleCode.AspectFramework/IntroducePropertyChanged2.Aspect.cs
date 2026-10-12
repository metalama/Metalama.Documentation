// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using System.ComponentModel;

namespace Doc.IntroducePropertyChanged2;

internal class IntroducePropertyChangedAspect : TypeAspect
{
    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        var propertyChangedEvent = builder.IntroduceEvent( nameof(this.PropertyChanged) )
            .Declaration;

        builder.IntroduceMethod(
            nameof(this.OnPropertyChanged),
            args: new { theEvent = propertyChangedEvent } );
    }

    [Template]
    public event PropertyChangedEventHandler? PropertyChanged;

    [Template]
    protected virtual void OnPropertyChanged( string propertyName, IEvent theEvent )
    {
        theEvent.Raise( meta.This, new PropertyChangedEventArgs( propertyName ) );
    }
}