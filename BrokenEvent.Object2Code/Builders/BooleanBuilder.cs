using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code.Builders
{
  class BooleanBuilder: IBuilder
  {
    public void Build(object target, IBuildContext context)
    {
      context.Append((bool)target ? "true" : "false");
    }
  }
}
