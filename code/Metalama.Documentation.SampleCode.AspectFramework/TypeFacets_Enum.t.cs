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
  public void Grant(string user, [DefinedEnumValue] AccessRights rights)
  {
    if (((long)rights & ~7L) != 0)
    {
      throw new ArgumentOutOfRangeException(nameof(rights), rights, "The value is not defined in the 'AccessRights' enum.");
    }
  }
}