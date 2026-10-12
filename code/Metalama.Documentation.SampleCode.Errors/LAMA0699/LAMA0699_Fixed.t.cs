using System;
namespace Doc.LAMA0699.Fixed;
[LogConstructorErrors]
public class DatabaseConnection
{
  public DatabaseConnection(string connectionString)
  {
    try
    {
      Console.WriteLine($"Connecting to '{connectionString}'.");
    }
    catch (Exception e)
    {
      Console.WriteLine($"DatabaseConnection.DatabaseConnection(string) failed: {e.Message}");
      throw;
    }
  }
}