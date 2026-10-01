// This is public domain Metalama sample code.

namespace Doc.RegistryStorage;

[RegistryStorage( "Animals" )]
internal class Animals
{
    public int Turtles { get; set; }

    public int Cats { get; set; }

    public int All => this.Turtles + this.Cats;
}