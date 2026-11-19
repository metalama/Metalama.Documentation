// Warning LAMA0891 on `_displayNames`: `'LogAttribute' is a run-time-or-compile-time type, but the static member '_displayNames' is of type 'Dictionary<string, string>', which is not immutable (path: LogAttribute -> _displayNames -> Dictionary<string, string> (a mutable collection; use the corresponding type of System.Collections.Immutable)). A static field of a compile-time type is shared by every aspect instance, every target and every compilation, and at design time it keeps its value for as long as the analysis process runs. Mark the member with [RunTime] so that it exists only at run time, or make it read-only and of an immutable type.`
// Warning LAMA0887 on `this._targetCount`: `'LogAttribute' must be written in an immutable style because it is an aspect, a fabric, a validator, or another type bound by the [ImmutableType] contract, but '_targetCount' is assigned here, outside a constructor. Move the assignment into a constructor or an 'init' accessor.`
using System;
namespace Doc.CompileTimeRules_Wrong;
public class C
{
  [Log]
  public void M()
  {
    Console.WriteLine("Executing C.M().");
  }
}