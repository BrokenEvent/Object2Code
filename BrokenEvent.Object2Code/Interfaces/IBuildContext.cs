using System;
using System.Text;

namespace BrokenEvent.Object2Code.Interfaces
{
  public interface IBuildContext
  {
    StringBuilder StringBuilder { get; }
    BuilderSettings Settings { get; }

    ITypeDictionary Dictionary { get; }

    void AppendTypeName(Type type);

    void Append(string text);

    void AppendContent(object target, bool useConstructor = true);

    void AppendIndent();

    void AppendLineBreak(bool indent = true);

    void IncreaseIndent(int number = 1);

    void DecreaseIndent(int number = 1);
  }
}
