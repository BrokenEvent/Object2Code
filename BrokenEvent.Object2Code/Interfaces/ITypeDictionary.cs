using System;

namespace BrokenEvent.Object2Code.Interfaces
{
  /// <summary>
  /// Provides builders for types.
  /// </summary>
  public interface ITypeDictionary
  {
    /// <summary>
    /// Gets the builder for the given type.
    /// </summary>
    /// <param name="type">Type to get builder for.</param>
    /// <returns>The builder able to build a value of given type.</returns>
    IBuilder GetBuilder(Type type);

    /// <summary>
    /// Get the keyword for the type.
    /// </summary>
    /// <param name="type">Type to get keyword for.</param>
    /// <returns>The keyword for given type or <c>null</c> if the type is non-system and keywords are not available.</returns>
    string GetKeywordForType(Type type);

    /// <summary>
    /// Gets the default value builder for given type.
    /// </summary>
    /// <param name="type">Type to get default builder for.</param>
    /// <returns>The default value builder (<c>null</c>, <c>default(int)</c>, etc.) for given type.</returns>
    IBuilder GetDefaultBuilder(Type type);
  }
}
