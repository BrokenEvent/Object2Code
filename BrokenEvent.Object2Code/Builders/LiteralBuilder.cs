using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code.Builders
{
  internal class LiteralBuilder : IBuilder
  {
    private readonly string quote;

    public LiteralBuilder(string quote)
    {
      this.quote = quote;
    }

    public void Build(object target, IBuildContext context)
    {
      context.Append(quote);
      context.Append(target.ToString());
      context.Append(quote);
    }
  }
}
