using System;

using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code.Builders
{
  internal class FlagsEnumBuilder : IBuilder
  {
    private readonly Type type;

    public FlagsEnumBuilder(Type type)
    {
      this.type = type;
    }

    public void Build(object target, IBuildContext context)
    {
      Array values = Enum.GetValues(type);
      string[] names = Enum.GetNames(type);

      int value = ((IConvertible)target).ToInt32(null);

      bool firstValue = true;

      for (int i = 0; i < values.Length; i++)
      {
        int flag = ((IConvertible)values.GetValue(i)).ToInt32(null);

        if ((value & flag) == 0)
          continue;

        if (!firstValue)
          context.Append(" | ");
        firstValue = false;

        context.AppendTypeName(type);
        context.Append(".");
        context.Append(names[i]);
      }

      if (firstValue)
        context.Append("0");
    }
  }
}
