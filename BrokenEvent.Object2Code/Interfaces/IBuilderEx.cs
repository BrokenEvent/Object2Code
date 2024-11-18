namespace BrokenEvent.Object2Code.Interfaces
{
  public interface IBuilderEx: IBuilder
  {
    void Build(object target, bool useConstructor, IBuildContext context);
  }
}
