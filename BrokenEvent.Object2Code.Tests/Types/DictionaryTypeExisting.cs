using System.Collections.Generic;

namespace BrokenEvent.Object2Code.Tests.Types
{
  class DictionaryTypeExisting
  {
    public string Name { get; set; }

    public Dictionary<string, SimpleType> Items { get; } = new Dictionary<string, SimpleType>();
  }
}
