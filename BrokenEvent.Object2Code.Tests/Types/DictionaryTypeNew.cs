using System.Collections.Generic;

namespace BrokenEvent.Object2Code.Tests.Types
{
  class DictionaryTypeNew
  {
    public string Name { get; set; }

    public Dictionary<string, SimpleType> Items { get; set; } = new Dictionary<string, SimpleType>();
  }
}
