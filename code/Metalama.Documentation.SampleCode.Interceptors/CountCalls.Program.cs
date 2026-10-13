// This is public domain Metalama sample code.

namespace Doc.CountCalls;

internal class Program
{
    private static void Main()
    {
        var service = new InvoiceService();

        for ( var i = 0; i < 3; i++ )
        {
            service.ComputeTotal( 100, "EU" );
        }

        CallCounter.Print();
    }
}
