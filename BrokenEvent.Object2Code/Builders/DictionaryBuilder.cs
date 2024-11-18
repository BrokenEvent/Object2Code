using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code.Builders
{
  internal class DictionaryBuilder : IBuilderEx
  {
    private readonly Type dictionaryType;
    private readonly Type keyType;
    private readonly Type valueType;
    private readonly PropertyInfo getKeyProperty;
    private readonly PropertyInfo getValueProperty;

    public DictionaryBuilder(Type dictionaryType, Type keyType, Type valueType)
    {
      this.dictionaryType = dictionaryType;
      this.keyType = keyType;
      this.valueType = valueType;

      Type itemType = typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType);
      getKeyProperty = itemType.GetProperty("Key");
      getValueProperty = itemType.GetProperty("Value");
    }

    public void Build(object target, bool useConstructor, IBuildContext context)
    {
      if (useConstructor)
      {
        context.Append("new ");
        context.AppendTypeName(dictionaryType);
        context.Append("<");
        context.AppendTypeName(keyType);
        context.Append(", ");
        context.AppendTypeName(valueType);
        context.Append(">");
        if (!context.Settings.SkipBracesForEmptyConstructor)
          context.Append("()");
      }
      context.AppendLineBreak();
      context.Append("{");
      context.IncreaseIndent();
      context.AppendLineBreak();

      IEnumerable e = target as IEnumerable;
      bool firstItem = true;

      foreach (object o in e)
      {
        if (!firstItem)
        {
          context.Append(",");
          context.AppendLineBreak();
        }
        firstItem = false;

        context.Append("{");
        context.IncreaseIndent();
        context.AppendLineBreak();
        context.AppendContent(getKeyProperty.GetValue(o));
        context.Append(",");
        context.AppendLineBreak();
        context.AppendContent(getValueProperty.GetValue(o));
        context.DecreaseIndent();
        context.AppendLineBreak();
        context.Append("}");
      }

      context.DecreaseIndent();
      context.AppendLineBreak();
      context.StringBuilder.Append("}");
    }

    public void Build(object target, IBuildContext context)
    {
      Build(target, true, context);
    }
  }
}
