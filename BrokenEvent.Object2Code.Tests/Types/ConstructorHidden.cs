namespace BrokenEvent.Object2Code.Tests.Types
{
  class ConstructorHidden
  {
    private readonly int intValue;

    public ConstructorHidden(int intValue)
    {
      this.intValue = intValue;
    }

    public string StringValue { get; set; }
  }
}
