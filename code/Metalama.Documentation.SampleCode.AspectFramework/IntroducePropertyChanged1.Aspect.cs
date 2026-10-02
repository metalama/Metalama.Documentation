// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.ComponentModel;

namespace Doc.IntroducePropertyChanged1;

internal class IntroducePropertyChangedAspect : TypeAspect
{
    [Introduce]
    public event PropertyChangedEventHandler? PropertyChanged;

    [Introduce]
    protected virtual void OnPropertyChanged( string propertyName )
    {
        this.PropertyChanged?.Invoke( meta.This, new PropertyChangedEventArgs( propertyName ) );
    }
}