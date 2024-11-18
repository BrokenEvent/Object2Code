using System;

using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code.Builders
{
  class DefaultBuilder: IBuilder
  {
    private readonly string value;
    private readonly Type type;

    public DefaultBuilder(string value)
    {
      this.value = value;
    }

    public DefaultBuilder(Type type)
    {
      this.type = type;
    }

    public void Build(object target, IBuildContext context)
    {
      if (value != null)
        context.Append(value);
      else
      {
        context.Append("default(");
        context.AppendTypeName(type);
        context.Append(")");
      }

      if (context.Settings.AddToDo)
        context.Append(" /* TODO */");
    }
  }
}
