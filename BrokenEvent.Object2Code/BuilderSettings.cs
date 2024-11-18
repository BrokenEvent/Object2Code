namespace BrokenEvent.Object2Code
{
  /// <summary>
  /// The code builder settings
  /// </summary>
  public class BuilderSettings
  {
    /// <summary>
    /// Gets or sets the null values handling rules.
    /// </summary>
    public NullValues NullValuesHandling { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether to insert full type names (namespace-qualified).
    /// </summary>
    /// <example>
    /// <para><c>true</c>: <c>BrokenEvent.Object2Code.Tests.Types.SimpleType Obj</c></para>
    /// <para><c>false</c>: <c>SimpleType Obj</c></para>
    /// </example>
    public bool UseFullNames { get; set; }

    /// <summary>
    /// Gets or sets the indentation value.
    /// </summary>
    public string Indent { get; set; } = "  ";

    /// <summary>
    /// Gets or sets the line break value.
    /// </summary>
    public string LineBreak { get; set; } = "\r\n";

    /// <summary>
    /// Gets or sets the TODO string.
    /// </summary>
    public string ToDo { get; set; } = "/* TODO */";

    /// <summary>
    /// Gets or sets the initial indentation level for the build.
    /// </summary>
    public int InitialIndentLevel { get; set; } = 2;

    /// <summary>
    /// Gets or sets the value indicating whether to skip braces for parameterless constructors.
    /// </summary>
    /// <example>
    /// <para><c>true</c>: <c>new SimpleType {...}</c></para>
    /// <para><c>false</c>: <c>new SimpleType() {...}</c></para>
    /// </example>
    public bool SkipBracesForEmptyConstructor { get; set; } = true;

    /// <summary>
    /// Gets or sets the value indicating whether to use keywords instead of primitive types.
    /// </summary>
    /// <example>
    /// <para><c>true</c>: <c>new int[] {...}</c></para>
    /// <para><c>false</c>: <c>new Int32[] {...}</c></para>
    /// </example>
    public bool KeywordsInsteadOfTypes { get; set; } = true;

    /// <summary>
    /// Gets the value indicating whether to add TODOs for unknown constructor parameters.
    /// </summary>
    /// <example>
    /// <para><c>true</c>: <c>new SimpleType(default(0) /* TODO */)</c></para>
    /// <para><c>false</c>: <c>new SimpleType(default(0))</c></para>
    /// </example>
    /// <seealso cref="ToDo"/>
    public bool AddToDo { get; set; }

    /// <summary>
    /// Whether to throw en exception when unable to find available public constructor.
    /// </summary>
    /// <remarks>If the value is <c>false</c> the code builder will produce uncompilable code with TODO remark (<see cref="AddToDo"/> setting is ignored).</remarks>
    /// <seealso cref="ToDo"/>
    public bool ThrowOnMissingConstructor { get; set; } = true;
  }

  public enum NullValues
  {
    /// <summary>
    /// Don't add property setters for null values.
    /// </summary>
    Ignore,
    /// <summary>
    /// Implicitly set nulls.
    /// </summary>
    Implicit,
  }
}
