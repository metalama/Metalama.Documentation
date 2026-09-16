// This is public domain Metalama sample code.

using System;

namespace Doc.Trimmed;

public class Details
{
    [Trim]
    public string? Code { get; set; }
}

public class Program
{
    public static void Main()
    {
        Details detail1 = new() { Code = "   GW12345  " };

        Console.WriteLine( $"Code='{detail1.Code}'" );
    }
}