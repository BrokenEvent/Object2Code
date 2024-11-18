using System;
using System.Text;

namespace BrokenEvent.Object2Code.Interfaces
{
  /// <summary>
  /// The code build context.
  /// </summary>
  public interface IBuildContext
  {
    /// <summary>
    /// The string builder used.
    /// </summary>
    StringBuilder StringBuilder { get; }

    /// <summary>
    /// The build settings used.
    /// </summary>
    BuilderSettings Settings { get; }

    /// <summary>
    /// The type dictionary used.
    /// </summary>
    ITypeDictionary Dictionary { get; }

    /// <summary>
    /// Appends the type name for the type to the string builder.
    /// </summary>
    /// <param name="type">Type which name to add</param>
    /// <remarks>The result depends on <see cref="BuilderSettings.UseFullNames"/>.</remarks>
    void AppendTypeName(Type type);

    /// <summary>
    /// Appends a string to the string builder.
    /// </summary>
    /// <param name="text">Text to append.</param>
    /// <remarks>See helper method. Equal to <c>context.StringBuilder.Append(text)</c></remarks>
    void Append(string text);

    /// <summary>
    /// Appends the set code for the given target value to the string builder.
    /// </summary>
    /// <param name="target">Target value to append.</param>
    /// <param name="useConstructor">Whether to use constructor (see <see cref="IBuilderEx"/>)</param>
    void AppendContent(object target, bool useConstructor = true);

    /// <summary>
    /// Appends an indent depending on current indentation level to the string builder.
    /// </summary>
    /// <seealso cref="BuilderSettings.Indent"/>
    void AppendIndent();

    /// <summary>
    /// Appends a linebreak and optional indent in start of the next line to the string builder.
    /// </summary>
    /// <param name="indent">Whether to add indent.</param>
    /// <seealso cref="AppendIndent"/>
    /// <seealso cref="BuilderSettings.Indent"/>
    void AppendLineBreak(bool indent = true);

    /// <summary>
    /// Increases the indentation level of the context.
    /// </summary>
    /// <param name="number">Number to increase by.</param>
    void IncreaseIndent(int number = 1);

    /// <summary>
    /// Decreases the indentation level of the context.
    /// </summary>
    /// <param name="number">Number to decrease by.</param>
    void DecreaseIndent(int number = 1);
  }
}
