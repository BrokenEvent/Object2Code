using System.Text;

using BrokenEvent.Object2Code.Interfaces;

namespace BrokenEvent.Object2Code
{
  public static class CodeBuilder
  {
    private static void AssumeArgs(ref BuilderSettings settings, ref ITypeDictionary dictionary)
    {
      if (settings == null)
        settings = new BuilderSettings();
      if (dictionary == null)
        dictionary = TypeDictionary.Instance;
    }

    /// <summary>
    /// Builds a constructor call for given value.
    /// </summary>
    /// <param name="target">Value to build for.</param>
    /// <param name="settings">Build settings for the build process. <c>null</c> means to use the default settings.</param>
    /// <param name="dictionary">Type dictionary to provider builders. <c>null</c> means to use the default type dictionary.</param>
    /// <returns>Constructor code call for the given value.</returns>
    /// <example>
    /// <code>new SimpleType(1) { Name = "test" }</code>
    /// </example>
    public static string BuildConstructorCall(object target, BuilderSettings settings = null, ITypeDictionary dictionary = null)
    {
      AssumeArgs(ref settings, ref dictionary);

      BuildContext context = new BuildContext(target, dictionary, settings, new StringBuilder());
      context.AppendContent(target);

      return context.StringBuilder.ToString();
    }

    /// <summary>
    /// Builds a static readonly field code for given value.
    /// </summary>
    /// <param name="target">Value to build for.</param>
    /// <param name="name">Name of field to build.</param>
    /// <param name="settings">Build settings for the build process. <c>null</c> means to use the default settings.</param>
    /// <param name="dictionary">Type dictionary to provider builders. <c>null</c> means to use the default type dictionary.</param>
    /// <returns>Static readonly field for given object.</returns>
    /// <example>
    /// <code>public static readonly SimpleType MyObject = new SimpleType(1) { Name = "test" };</code>
    /// </example>
    public static string BuildStaticReadOnly(object target, string name, BuilderSettings settings = null, ITypeDictionary dictionary = null)
    {
      StringBuilder sb = new StringBuilder();
      BuildStaticReadOnly(target, name, sb, settings, dictionary);
      return sb.ToString();
    }

    /// <summary>
    /// Builds a static readonly field code for given value.
    /// </summary>
    /// <param name="target">Value to build for.</param>
    /// <param name="name">Name of field to build.</param>
    /// <param name="stringBuilder">String builder to build to.</param>
    /// <param name="settings">Build settings for the build process. <c>null</c> means to use the default settings.</param>
    /// <param name="dictionary">Type dictionary to provider builders. <c>null</c> means to use the default type dictionary.</param>
    /// <example>
    /// <code>public static readonly SimpleType MyObject = new SimpleType(1) { Name = "test" };</code>
    /// </example>
    public static void BuildStaticReadOnly(object target, string name, StringBuilder stringBuilder, BuilderSettings settings = null, ITypeDictionary dictionary = null)
    {
      AssumeArgs(ref settings, ref dictionary);

      BuildContext context = new BuildContext(target, dictionary, settings, stringBuilder);

      context.AppendIndent();
      context.Append("public static readonly ");
      context.AppendTypeName(target.GetType());
      context.Append(" ");
      context.Append(name);
      context.Append(" = ");
      context.AppendContent(target);
      context.Append(";");
    }
  }
}
