using System;
namespace Doc.BeforeTypeConstructor;
[RegisterMessageHandler]
public partial class Handler<TMessage>
  where TMessage : IMessage
{
  public void Handle(TMessage message)
  {
    Console.WriteLine($"Handling {message}.");
  }
  static Handler()
  {
    MessageRouter.Register<Handler<TMessage>, TMessage>();
  }
}