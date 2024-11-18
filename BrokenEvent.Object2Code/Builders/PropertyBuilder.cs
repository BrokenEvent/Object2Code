using System.Reflection;

using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code.Builders
{
  internal class PropertyBuilder : IBuilder
  {
    protected readonly PropertyInfo property;

    public PropertyBuilder(PropertyInfo property)
    {
      this.property = property;
    }

    public object Get(object target)
    {
      return property.GetValue(target);
    }

    public bool WillBuild(object value, IBuildContext context)
    {
      // we can build code for get-only properties, in certain cases
      if (!property.CanWrite)
      {
        IBuilder builder = context.Dictionary.GetBuilder(property.PropertyType);

        // IBuilderEx can handle assigment-less syntax
        return builder is IBuilderEx;
      }

      // regular case
      return value != null || context.Settings.NullValuesHandling != NullValues.Ignore;
    }

    public void Build(object target, IBuildContext context)
    {
      context.Append(property.Name);
      context.Append(" = ");

      context.AppendContent(target, property.CanWrite);
    }
  }
}
