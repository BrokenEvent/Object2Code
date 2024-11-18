using System;

using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code.Builders
{
  internal class EnumBuilder : IBuilder
  {
    private readonly Type type;

    public EnumBuilder(Type type)
    {
      this.type = type;
    }

    public void Build(object target, IBuildContext context)
    {
      context.AppendTypeName(type);
      context.Append(".");
      context.Append(target.ToString());
    }
  }
}
