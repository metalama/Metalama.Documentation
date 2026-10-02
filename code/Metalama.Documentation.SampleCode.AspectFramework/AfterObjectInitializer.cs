// This is public domain Metalama sample code.

using System;

namespace Doc.AfterObjectInitializer;

[PublishWhenInitialized]
public partial record Document
{
    public required string Id { get; init; }

    public string Title { get; init; } = "Untitled";
}

public partial record Report : Document
{
    public required DateOnly Date { get; init; }
}
