using System;
using System.Collections;

using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code.Builders
{
  internal class CollectionBuilder : IBuilderEx
  {
    private readonly Type type;

    public CollectionBuilder(Type type)
    {
      this.type = type;
    }

    public void Build(object target, bool useConstructor, IBuildContext context)
    {
      if (useConstructor)
      {
        context.Append("new ");
        context.AppendTypeName(type);
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

        context.AppendContent(o);
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
