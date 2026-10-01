// This is public domain Metalama sample code.

using System;
using System.Collections.Generic;

namespace Doc.BeforeTypeConstructor;

public interface IMessage;

public record OrderPlaced( string OrderId ) : IMessage;

public record OrderShipped( string OrderId ) : IMessage;

public static class MessageRouter
{
    private static readonly Dictionary<Type, Type> _handlerTypes = new();

    public static void Register<THandler, TMessage>()
        where THandler : new()
        where TMessage : IMessage
    {
        lock ( _handlerTypes )
        {
            _handlerTypes[typeof(TMessage)] = typeof(THandler);
        }
    }

    public static void Dispatch( IMessage message )
    {
        Type? handlerType;

        lock ( _handlerTypes )
        {
            if ( !_handlerTypes.TryGetValue( message.GetType(), out handlerType ) )
            {
                Console.WriteLine( $"No handler for {message.GetType().Name}." );

                return;
            }
        }

        var handler = Activator.CreateInstance( handlerType )!;
        var handleMethod = handlerType.GetMethod( "Handle" )!;
        handleMethod.Invoke( handler, new object[] { message } );
    }
}
