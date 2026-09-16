// This is public domain Metalama sample code.

using System;

namespace Doc.Authorize;

public class Program
{
    [Authorize]
    public static void SaveIdentityDetails()
    {
        // A sensitive method that should 
        // ideally only be called by the current user.
        Console.WriteLine( "Saving identity details..." );
    }

    public static void Main()
    {
        try
        {
            SaveIdentityDetails();
        }
        catch ( Exception e )
        {
            Console.Error.WriteLine( e.ToString() );
        }
    }
}