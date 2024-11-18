using System;
using System.Collections.Generic;
using System.Reflection;

using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code.Builders
{
  internal class ComplexTypeBuilder : IBuilder
  {
    private readonly Type type;
    private readonly List<PropertyBuilder> properties = new List<PropertyBuilder>();

    private readonly List<IBuilder> constructorArgs = new List<IBuilder>();
    private readonly ConstructorInfo preferredConstructor;

    public ComplexTypeBuilder(Type type, ITypeDictionary dictionary)
    {
      this.type = type;

      Dictionary<string, PropertyInfo> props = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);

      // collect total list of properties
      foreach (PropertyInfo info in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
      {
        ParameterInfo[] indices = info.GetIndexParameters();

        // we ignore all sorts of this[...]
        if (indices.Length > 0)
          continue;

        props.Add(info.Name, info);
      }

      ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

      foreach (ConstructorInfo constructor in constructors)
      {
        ParameterInfo[] parameters = constructor.GetParameters();
        if (parameters.Length == 0)
          continue; // use the default constructor as the last resort

        if (!CheckConstructor(parameters, props))
          continue; // we can't use this constructor

        preferredConstructor = constructor;
        break;
      }

      // we didn't find the constructor? then use a random one
      if (preferredConstructor == null)
        foreach (ConstructorInfo constructor in constructors)
        {
          preferredConstructor = constructor;
          break;
        }

      // use this constructor (remove used properties from the list)
      if (preferredConstructor != null)
        UseConstructor(preferredConstructor.GetParameters(), props, constructorArgs, dictionary);

      // fill the list with remaining properties
      foreach (PropertyInfo info in props.Values)
        properties.Add(new PropertyBuilder(info));
    }

    private static bool CheckConstructor(ParameterInfo[] parameters, IDictionary<string, PropertyInfo> props)
    {
      foreach (ParameterInfo parameter in parameters)
        if (!props.ContainsKey(parameter.Name))
          return false; // not found

      return true;
    }

    private static void UseConstructor(ParameterInfo[] parameters, IDictionary<string, PropertyInfo> props, ICollection<IBuilder> args, ITypeDictionary dictionary)
    {
      foreach (ParameterInfo parameter in parameters)
      {
        PropertyInfo prop;
        if (props.TryGetValue(parameter.Name, out prop))
        {
          // remove from props
          props.Remove(parameter.Name);
          args.Add(new ArgBuilder(prop));
        }
        else
        {
          // use default value
          args.Add(dictionary.GetDefaultBuilder(parameter.ParameterType));
        }
      }
    }

    private void BuildConstructor(object target, IBuildContext context)
    {
      context.Append("new ");
      context.AppendTypeName(type);

      if (preferredConstructor == null && context.Settings.ThrowOnMissingConstructor)
        throw new InvalidOperationException($"Unable to find usable constructor for {type.FullName}");

      if (constructorArgs.Count == 0)
      {
        if (properties.Count == 0 || !context.Settings.SkipBracesForEmptyConstructor || preferredConstructor == null)
          context.Append("()");

        if (preferredConstructor == null)
          context.Append(context.Settings.ToDo);

        return;
      }

      context.Append("(");
      context.IncreaseIndent(2);
      context.AppendLineBreak();

      bool firstArg = true;

      foreach (IBuilder arg in constructorArgs)
      {
        if (!firstArg)
        {
          context.Append(",");
          context.AppendLineBreak();
        }
        firstArg = false;

        arg.Build(target, context);
      }

      context.DecreaseIndent();
      context.AppendLineBreak();
      context.Append(")");
      context.DecreaseIndent();
    }

    public void Build(object target, IBuildContext context)
    {
      BuildConstructor(target, context);

      if (properties.Count == 0)
        return;

      bool firstProperty = true;

      foreach (PropertyBuilder property in properties)
      {
        object value = property.Get(target);

        if (!property.WillBuild(value, context))
          continue;

        if (firstProperty)
        {
          context.AppendLineBreak();
          context.Append("{");
          context.IncreaseIndent();
          context.AppendLineBreak();
        }
        else
        {
          context.Append(",");
          context.AppendLineBreak();
        }
        firstProperty = false;

        property.Build(value, context);
      }

      if (firstProperty)
        return;

      context.DecreaseIndent();
      context.AppendLineBreak();

      context.Append("}");
    }
  }
}
