using System;

namespace BrokenEvent.Object2Code.Interfaces
{
  public interface ITypeDictionary
  {
    IBuilder GetBuilder(Type type);

    string GetKeywordForType(Type type);

    IBuilder GetDefaultBuilder(Type type);
  }
}
