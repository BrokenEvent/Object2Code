using System;
using System.Collections.Generic;

using BrokenEvent.Object2Code.Builders;
using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code
{
  class TypeDictionary: ITypeDictionary
  {
    private readonly Dictionary<Type, IBuilder> builders = new Dictionary<Type, IBuilder>();
    private readonly PrimitiveBuilder primitiveBuilder = new PrimitiveBuilder();
    private readonly LiteralBuilder stringBuilder = new LiteralBuilder("\"");
    private readonly LiteralBuilder charBuilder = new LiteralBuilder("'");

    private readonly Dictionary<Type, string> keywords = new Dictionary<Type, string>
    {
      { typeof(int), "int" },
      { typeof(uint), "uint" },
      { typeof(byte), "byte" },
      { typeof(sbyte), "sbyte" },
      { typeof(short), "short" },
      { typeof(ushort), "ushort" },
      { typeof(long), "long" },
      { typeof(ulong), "ulong" },
      { typeof(bool), "bool" },
      { typeof(float), "float" },
      { typeof(double), "double" },
      { typeof(string), "string" }
    };

    private readonly Dictionary<Type, IBuilder> defaultBuilders = new Dictionary<Type, IBuilder>();
    private readonly DefaultBuilder defaultClassBuilder = new DefaultBuilder("null");

    public IBuilder GetBuilder(Type type)
    {
      if (type == typeof(string))
        return stringBuilder;
      if (type == typeof(char))
        return charBuilder;

      if (type.IsPrimitive)
        return primitiveBuilder;

      IBuilder result;
      if (!builders.TryGetValue(type, out result))
      {
        result = CreateTypeBuilder(type);
        builders.Add(type, result);
      }

      return result;
    }

    private IBuilder CreateTypeBuilder(Type type)
    {
      if (type.IsArray)
        return new ArrayBuilder(type);

      if (type.IsEnum)
        return new EnumBuilder(type);

      foreach (Type iface in type.GetInterfaces())
      {
        if (!iface.IsGenericType)
          continue;

        Type genericType = iface.GetGenericTypeDefinition();

        if (genericType == typeof(ICollection<>))
        {
          Type innerType = iface.GetGenericArguments()[0];
          // IDictionary<A,B> is also an ICollection<KeyValuePair<A,B>, so ignore it
          if (innerType.IsGenericType && innerType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            continue;

          return new CollectionBuilder(type.GetGenericTypeDefinition(), innerType);
        }

        if (genericType == typeof(IDictionary<,>))
        {
          Type[] innerTypes = iface.GetGenericArguments();
          return new DictionaryBuilder(type, innerTypes[0], innerTypes[1]);
        }
      }

      return new ComplexTypeBuilder(type, this);
    }

    public string GetKeywordForType(Type type)
    {
      return keywords.TryGetValue(type, out string result) ? result : null;
    }

    public IBuilder GetDefaultBuilder(Type type)
    {
      if (type.IsClass)
        return defaultClassBuilder;

      IBuilder builder;
      if (defaultBuilders.TryGetValue(type, out builder))
        return builder;

      builder = new DefaultBuilder(type);
      defaultBuilders.Add(type, builder);

      return builder;
    }

    public static TypeDictionary Instance { get; } = new TypeDictionary();
  }
}
