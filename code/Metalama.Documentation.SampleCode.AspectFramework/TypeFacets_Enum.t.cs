using System;
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
  private Priority _priority;
  [DefinedEnumValue]
  public Priority Priority
  {
    get
    {
      return _priority;
    }
    set
    {
      if (value is not (Priority.Low or Priority.Normal or Priority.High))
      {
        throw new ArgumentOutOfRangeException(nameof(value), value, "The value is not defined in the 'Priority' enum.");
      }
      _priority = value;
    }
  }
  public void SetState([DefinedEnumValue] JobState state)
  {
    if (state is not (JobState.Pending or JobState.Running or JobState.Completed))
    {
      throw new ArgumentOutOfRangeException(nameof(state), state, "The value is not defined in the 'JobState' enum.");
    }
  }
}