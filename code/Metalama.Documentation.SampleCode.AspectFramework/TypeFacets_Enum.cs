// This is public domain Metalama sample code.

namespace Doc.TypeFacets_Enum;

public enum Priority : byte
{
    Low = 1,
    Normal = 2,
    High = 3
}

public enum JobState
{
    Pending,
    Running,
    Completed
}

public class Job
{
    [DefinedEnumValue]
    public Priority Priority { get; set; }

    public void SetState( [DefinedEnumValue] JobState state ) { }
}
