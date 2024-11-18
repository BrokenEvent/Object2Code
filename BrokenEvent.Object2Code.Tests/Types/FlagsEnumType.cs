using System;

namespace BrokenEvent.Object2Code.Tests.Types
{
  class FlagsEnumType
  {
    public FlagsEnum Value { get; set; }
  }

  [Flags]
  enum FlagsEnum
  {
    OneValue = 1 << 0,
    AnotherValue = 1 << 1,
  }
}
