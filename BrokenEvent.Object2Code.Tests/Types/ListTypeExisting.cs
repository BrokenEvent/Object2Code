using System.Collections.Generic;

namespace BrokenEvent.Object2Code.Tests.Types
{
  class ListTypeExisting
  {
    public string Name { get; set; }

    public List<SimpleType> Items { get; } = new List<SimpleType>();
  }
}
