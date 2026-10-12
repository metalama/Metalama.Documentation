// This is public domain Metalama sample code.

using System.ComponentModel.DataAnnotations;

namespace Doc.Builder_;

[Builder]
internal partial class Material
{
    [Required]
    public string Name { get; }

    public double Density { get; }
}

internal static class Program
{
    public static void Main()
    {
#if TESTRUNNER
        var material = new Material.Builder( "Steel" ) { Density = 7.87 }.Build();
#endif
    }
}
