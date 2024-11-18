namespace BrokenEvent.Object2Code.Tests.Types
{
  class ConstructorTypeAllReadOnly
  {
    public ConstructorTypeAllReadOnly(int intValue, string stringValue)
    {
      IntValue = intValue;
      StringValue = stringValue;
    }

    public int IntValue { get; }

    public string StringValue { get; }
  }
}
