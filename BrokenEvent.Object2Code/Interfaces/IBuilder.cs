namespace BrokenEvent.Object2Code.Interfaces
{
  /// <summary>
  /// Builds a code fragment for a given value.
  /// </summary>
  public interface IBuilder
  {
    /// <summary>
    /// Builds a code fragment for a given value.
    /// </summary>
    /// <param name="target">The target value fo build for.</param>
    /// <param name="context">Build context.</param>
    void Build(object target, IBuildContext context);
  }
}
