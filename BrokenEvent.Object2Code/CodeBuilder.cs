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

    public static string BuildConstructorCall(object target, BuilderSettings settings = null, ITypeDictionary dictionary = null)
    {
      AssumeArgs(ref settings, ref dictionary);

      BuildContext context = new BuildContext(target, dictionary, settings);
      context.AppendContent(target);

      return context.StringBuilder.ToString();
    }

    public static string BuildStaticReadOnly(object target, string name, BuilderSettings settings = null, ITypeDictionary dictionary = null)
    {
      AssumeArgs(ref settings, ref dictionary);

      BuildContext context = new BuildContext(target, dictionary, settings);

      context.AppendIndent();
      context.Append("public static readonly ");
      context.AppendTypeName(target.GetType());
      context.Append(" ");
      context.Append(name);
      context.Append(" = ");
      context.AppendContent(target);
      context.Append(";");

      return context.StringBuilder.ToString();
    }
  }
}
