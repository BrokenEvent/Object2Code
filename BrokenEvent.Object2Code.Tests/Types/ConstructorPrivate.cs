namespace BrokenEvent.Object2Code.Tests.Types
{
  class ConstructorPrivate
  {
    private ConstructorPrivate(int intValue)
    {
      IntValue = intValue;
    }

    public int IntValue { get; }

    public static ConstructorPrivate Create(int value)
    {
      return new ConstructorPrivate(value);
    }
  }
}
