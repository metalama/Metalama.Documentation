// This is public domain Metalama sample code.

using Metalama.Patterns.Contracts;

namespace Doc.Contracts.Input;

public class Customer
{
    [Phone]
    public string? Phone { get; set; }

    [Url]
    public string? Url { get; set; }

    [Range( 1900, 2100 )]
    public int? BirthYear { get; set; }

    public string? FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public Customer( [Required] string fullName )
    {
        var split = fullName.Split( ' ' );

        if ( split.Length == 0 )
        {
            this.FirstName = "";
            this.LastName = split[0];
        }
        else
        {
            this.FirstName = split[0];
            this.LastName = split[^1];
        }
    }
}