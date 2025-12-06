// This is public domain Metalama sample code.

using System;

namespace Doc.StaticProxy;

public interface IPropertyStore
{
    object Get( string name );

    void Store( string name, object value );
}

public interface IInterceptor
{
    T Invoke<T>( Func<T> next );

    void Invoke( Action next );
}

[ProxyAspect( typeof(IPropertyStore) )]
public class PropertyStoreProxy { }