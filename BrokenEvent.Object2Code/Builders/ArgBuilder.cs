using System.Reflection;

using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code.Builders
{
  class ArgBuilder: IBuilder
  {
    private readonly PropertyInfo property;

    public ArgBuilder(PropertyInfo property)
    {
      this.property = property;
    }

    public void Build(object target, IBuildContext context)
    {
      object value = property.GetValue(target);
      context.AppendContent(value);
    }
  }
}
