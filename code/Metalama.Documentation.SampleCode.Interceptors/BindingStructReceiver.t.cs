using System;
namespace Doc.BindingStructReceiver;
[TraceCounters]
public class Statistics
{
  private Counter _visits;
  public int RecordVisit()
  {
    CounterTracing.Increment(ref this._visits);
    return this._visits.Value;
  }
}
public struct Counter
{
  public int Value { get; private set; }
  public void Increment() => this.Value++;
  public void Add(int amount) => this.Value += amount;
}
internal class Program
{
  private static void Main()
  {
    var statistics = new Statistics();
    statistics.RecordVisit();
    Console.WriteLine($"Visits: {statistics.RecordVisit()}");
  }
}