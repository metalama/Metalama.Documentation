using System;
using System.Collections.Generic;
namespace Doc.IntroduceOperator;
[Equatable]
internal partial class Person : IEquatable<Person>
{
  public string Name { get; }
  public int Age { get; }
  public Person(string name, int age)
  {
    Name = name;
    Age = age;
  }
  public override bool Equals(object? obj)
  {
    if (obj is null || obj.GetType() != typeof(Person))
    {
      return false;
    }
    return Equals((Person)obj);
  }
  public bool Equals(Person other)
  {
    if (other is null)
    {
      return false;
    }
    if (!EqualityComparer<string>.Default.Equals(Name, other.Name))
    {
      return false;
    }
    if (!EqualityComparer<int>.Default.Equals(Age, other.Age))
    {
      return false;
    }
    return true;
  }
  public override int GetHashCode()
  {
    var hashCode = new HashCode();
    hashCode.Add(Name);
    hashCode.Add(Age);
    return hashCode.ToHashCode();
  }
  public static bool operator ==(Person left, Person right)
  {
    return Equals(left, right);
  }
  public static bool operator !=(Person left, Person right)
  {
    return !Equals(left, right);
  }
}
internal class Program
{
  private static void Main()
  {
    var p1 = new Person("Alice", 30);
    var p2 = new Person("Alice", 30);
    var p3 = new Person("Bob", 25);
    Console.WriteLine($"p1 == p2: {p1 == p2}");
    Console.WriteLine($"p1 != p3: {p1 != p3}");
    Console.WriteLine($"p1.GetHashCode() == p2.GetHashCode(): {p1.GetHashCode() == p2.GetHashCode()}");
  }
}