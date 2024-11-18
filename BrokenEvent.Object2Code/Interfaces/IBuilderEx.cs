namespace BrokenEvent.Object2Code.Interfaces
{
  /// <summary>
  /// Extented version of builder which allows to skip object creation code.
  /// </summary>
  public interface IBuilderEx: IBuilder
  {
    /// <summary>
    /// Builds a code fragment for a given value.
    /// </summary>
    /// <param name="target">The value to build for.</param>
    /// <param name="useConstructor">Whether to use the constructor. If the value is false, no <c>new ...</c> code will be generated.</param>
    /// <param name="context">Build context.</param>
    void Build(object target, bool useConstructor, IBuildContext context);
  }
}
