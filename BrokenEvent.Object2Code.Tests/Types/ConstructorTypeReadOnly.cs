namespace BrokenEvent.Object2Code.Tests.Types
{
  class ConstructorTypeReadOnly
  {
    public ConstructorTypeReadOnly(int intValue)
    {
      IntValue = intValue;
    }

    public int IntValue { get; }

    public string StringValue { get; set; }
  }
}