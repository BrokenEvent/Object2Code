namespace BrokenEvent.Object2Code.Tests.Types
{
  class SimpleType
  {
    public int IntValue { get; set; }

    public string StringValue { get; set; }

    public char CharValue { get; set; }

    public int ReadOnlyValue { get; } = 100;

    public SimpleEnum EnumValue { get; set; }
  }

  enum SimpleEnum
  {
    SomeValue,
    SomeOtherValue,
  }
}
