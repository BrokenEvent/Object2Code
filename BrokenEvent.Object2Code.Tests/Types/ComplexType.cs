namespace BrokenEvent.Object2Code.Tests.Types
{
  class ComplexType
  {
    public string Name { get; set; }

    public SimpleType EmbeddedSimple { get; set; }

    public ComplexType EmbeddedComplex { get; set; }
  }
}
