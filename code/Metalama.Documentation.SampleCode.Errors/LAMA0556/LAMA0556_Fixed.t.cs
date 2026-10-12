using System.Diagnostics.CodeAnalysis;
namespace Doc.LAMA0556.Fixed;
[GenerateRecord]
public partial class OrderService
{
  public record OrderLine([property: ExcludeFromCodeCoverage] int Quantity)
  {
  }
}