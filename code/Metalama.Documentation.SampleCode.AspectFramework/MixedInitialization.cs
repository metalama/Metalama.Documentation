// This is public domain Metalama sample code.

using System;
using System.Collections.Generic;
using Metalama.Framework.RunTime.Initialization;

namespace Doc.MixedInitialization;

[TrackLifecycle]
public sealed partial class Customer : IInitializable
{
    private List<string>? _tags = new();

    public Customer( int id )
    {
        this.Id = id;
    }

    public int Id { get; }

    public string FirstName { get; init; } = "";

    public string LastName { get; init; } = "";

    public string Email { get; init; } = "";

    public IReadOnlyList<string> Tags { get; private set; } = null!;

    public void OnConstructed( InitializationContext context = default )
    {
        // Once all constructors have run, the tag list is frozen.
        this.Tags = ( this._tags ?? new List<string>() ).AsReadOnly();
        this._tags = null;
    }

    public void Initialize( InitializationContext context = default )
    {
        // Cross-property validation: identity requires Email, or both names.
        var hasEmail = !string.IsNullOrEmpty( this.Email );
        var hasFullName = !string.IsNullOrEmpty( this.FirstName )
                          && !string.IsNullOrEmpty( this.LastName );

        if ( !hasEmail && !hasFullName )
        {
            throw new InvalidOperationException(
                "A customer needs either an Email or both FirstName and LastName." );
        }
    }
}
