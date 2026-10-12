namespace Doc.LAMA0533.Fixed;
// The [Validatable] aspect introduces an abstract Validate method into this class.
[Validatable]
internal abstract class Entity
{
  public int Id { get; set; }
  public abstract void Validate();
}