using System;

using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code.Builders
{
  class ArrayBuilder: IBuilder
  {
    private readonly Type type;

    public ArrayBuilder(Type type)
    {
      this.type = type.GetElementType();
    }

    public void Build(object target, IBuildContext context)
    {
      context.Append("new ");
      context.AppendTypeName(type);
      context.Append("[]");
      context.AppendLineBreak();
      context.Append("{");
      context.IncreaseIndent();
      context.AppendLineBreak();

      Array array = target as Array;
      bool firstItem = true;

      foreach (object o in array)
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
  }
}
