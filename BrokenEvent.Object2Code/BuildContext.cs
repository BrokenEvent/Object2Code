using System;
using System.Text;

using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code
{
  internal class BuildContext: IBuildContext
  {
    private int indentLevel;
    private StringBuilder stringBuilder = new StringBuilder();

    public BuildContext(object target, ITypeDictionary dictionary, BuilderSettings settings)
    {
      Dictionary = dictionary;
      indentLevel = settings.InitialIndentLevel;
      Settings = settings;
    }

    public StringBuilder StringBuilder
    {
      get { return stringBuilder; }
    }

    public BuilderSettings Settings { get; }

    public ITypeDictionary Dictionary { get; }

    public void AppendTypeName(Type type)
    {
      if (Settings.KeywordsInsteadOfTypes)
      {
        string keyword = Dictionary.GetKeywordForType(type);
        if (keyword != null)
        {
          stringBuilder.Append(keyword);
          return;
        }
      }

      string name = Settings.UseFullNames ? type.FullName : type.Name;

      // fix for generics like List`1
      int index = name.LastIndexOf('`');
      if (index != -1)
        name = name.Substring(0, index);

      stringBuilder.Append(name);
    }

    public void Append(string text)
    {
      stringBuilder.Append(text);
    }

    public void AppendContent(object target)
    {
      if (target == null)
      {
        stringBuilder.Append("null");
        return;
      }

      IBuilder builder = Dictionary.GetBuilder(target.GetType());
      builder.Build(target, this);
    }

    public void AppendIndent()
    {
      for (int i = 0; i < indentLevel; i++)
        stringBuilder.Append(Settings.Indent);
    }

    public void AppendLineBreak(bool indent = true)
    {
      stringBuilder.Append(Settings.LineBreak);
      if (indent)
        AppendIndent();
    }

    public void IncreaseIndent(int number = 1)
    {
      indentLevel += number;
    }

    public void DecreaseIndent(int number = 1)
    {
      indentLevel -= number;
    }
  }
}
