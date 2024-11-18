namespace BrokenEvent.Object2Code.Tests.Types
{
  class GenericType<T1, T2>
  {
    public GenericType(T1 name)
    {
      Name = name;
    }

    public T1 Name { get; }
    public T2 Value { get; set; }
  }
}
