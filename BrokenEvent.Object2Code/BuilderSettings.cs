namespace BrokenEvent.Object2Code
{
  public class BuilderSettings
  {
    public NullValues NullValuesHandling { get; set; }

    public bool UseFullNames { get; set; }

    public string Indent { get; set; } = "  ";

    public string LineBreak { get; set; } = "\r\n";

    public int InitialIndentLevel { get; set; } = 2;

    public bool SkipBracesForEmptyConstructor { get; set; } = true;

    public bool KeywordsInsteadOfTypes { get; set; } = true;

    public bool AddToDo { get; set; }

    public bool ThrowOnMissingConstructor { get; set; } = true;
  }

  public enum NullValues
  {
    Implicit,
    Ignore,
  }
}
