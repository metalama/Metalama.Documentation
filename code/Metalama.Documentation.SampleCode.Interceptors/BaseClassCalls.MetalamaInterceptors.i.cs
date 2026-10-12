using System;
using Doc.BaseClassCalls;
internal static class MetalamaInterceptors
{
  internal static void Control_Invalidate_Interceptor(Control receiver)
  {
    Console.WriteLine("Repaint requested by OrderGrid.AddRow.");
    receiver.Invalidate();
  }
}