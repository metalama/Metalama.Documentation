namespace Doc.LAMA0019.Fixed;
internal class RetryPolicy
{
  private int _maxAttempts;
  public int MaxAttempts { get => this._maxAttempts; set => this._maxAttempts = value; }
  // Fixed: the aspect passes the field, which is a variable, to the 'out' parameter.
  [ParseInto(nameof(_maxAttempts))]
  public void Configure(string text)
  {
    int.TryParse(text, out _maxAttempts);
  }
}