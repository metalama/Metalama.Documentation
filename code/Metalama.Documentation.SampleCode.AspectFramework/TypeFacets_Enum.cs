// This is public domain Metalama sample code.

using System;

namespace Doc.TypeFacets_Enum;

public enum Priority : byte
{
    Low = 1,
    Normal = 2,
    High = 3
}

[Flags]
public enum AccessRights
{
    None = 0,
    Read = 1,
    Write = 2,
    Execute = 4
}

public class Job
{
    [DefinedEnumValue]
    public Priority Priority { get; set; }

    public void Grant( string user, [DefinedEnumValue] AccessRights rights ) { }
}
