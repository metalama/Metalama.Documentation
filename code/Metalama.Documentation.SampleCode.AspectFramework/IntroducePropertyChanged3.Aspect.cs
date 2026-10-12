// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using System.ComponentModel;

namespace Doc.IntroducePropertyChanged3;

internal class IntroducePropertyChangedAspect : TypeAspect
{
    [Introduce]
    public event PropertyChangedEventHandler? PropertyChanged;

    [Introduce]
    protected virtual void OnPropertyChanged( string propertyName )
    {
        meta.This.PropertyChanged?.Invoke(
            meta.This,
            new PropertyChangedEventArgs( propertyName ) );
    }
}