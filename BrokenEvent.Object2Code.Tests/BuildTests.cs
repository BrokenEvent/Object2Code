using System.Collections.Generic;

using BrokenEvent.Object2Code.Tests.Types;

using NUnit.Framework;

namespace BrokenEvent.Object2Code.Tests
{
  [TestFixture]
  class BuildTests
  {
    [Test]
    public void BuildSimple()
    {
      SimpleType obj = new SimpleType
      {
        IntValue = 25,
        StringValue = "Hello World",
        CharValue = '!'
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "Simple");
      const string expected = 
@"    public static readonly SimpleType Simple = new SimpleType
    {
      IntValue = 25,
      StringValue = ""Hello World"",
      CharValue = '!',
      EnumValue = SimpleEnum.SomeValue
    };";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildSimpleFullName()
    {
      SimpleType obj = new SimpleType
      {
        IntValue = 25,
        StringValue = "Hello World",
        CharValue = '!'
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "Simple", new BuilderSettings() { UseFullNames = true });
      const string expected =
@"    public static readonly BrokenEvent.Object2Code.Tests.Types.SimpleType Simple = new BrokenEvent.Object2Code.Tests.Types.SimpleType
    {
      IntValue = 25,
      StringValue = ""Hello World"",
      CharValue = '!',
      EnumValue = BrokenEvent.Object2Code.Tests.Types.SimpleEnum.SomeValue
    };";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildSimpleBraces()
    {
      SimpleType obj = new SimpleType
      {
        IntValue = 25,
        StringValue = "Hello World",
        CharValue = '!'
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "Simple", new BuilderSettings { SkipBracesForEmptyConstructor = false });
      const string expected =
@"    public static readonly SimpleType Simple = new SimpleType()
    {
      IntValue = 25,
      StringValue = ""Hello World"",
      CharValue = '!',
      EnumValue = SimpleEnum.SomeValue
    };";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildComplex()
    {
      ComplexType obj = new ComplexType
      {
        Name = "test",
        EmbeddedSimple = new SimpleType
        {
          IntValue = 46,
          StringValue = "Hello World",
          CharValue = '!'
        }
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "Simple");
      const string expected =
        @"    public static readonly ComplexType Simple = new ComplexType
    {
      Name = ""test"",
      EmbeddedSimple = new SimpleType
      {
        IntValue = 46,
        StringValue = ""Hello World"",
        CharValue = '!',
        EnumValue = SimpleEnum.SomeValue
      },
      EmbeddedComplex = null
    };";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildComplexNoNulls()
    {
      ComplexType obj = new ComplexType
      {
        Name = "test",
        EmbeddedSimple = new SimpleType
        {
          IntValue = 46,
          StringValue = "Hello World",
          CharValue = '!'
        }
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "Simple", new BuilderSettings(){NullValuesHandling = NullValues.Ignore});
      const string expected =
@"    public static readonly ComplexType Simple = new ComplexType
    {
      Name = ""test"",
      EmbeddedSimple = new SimpleType
      {
        IntValue = 46,
        StringValue = ""Hello World"",
        CharValue = '!',
        EnumValue = SimpleEnum.SomeValue
      }
    };";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildMoreComplex()
    {
      ComplexType obj = new ComplexType
      {
        Name = "test",
        EmbeddedSimple = new SimpleType
        {
          IntValue = 46,
          StringValue = "Hello World",
          CharValue = '!'
        },
        EmbeddedComplex = new ComplexType
        {
          Name = "embedded",
          EmbeddedSimple = new SimpleType
          {
            IntValue = 269,
            StringValue = "Bye world",
            CharValue = '~',
            EnumValue = SimpleEnum.SomeOtherValue
          },
        }
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "Simple");
      const string expected =
@"    public static readonly ComplexType Simple = new ComplexType
    {
      Name = ""test"",
      EmbeddedSimple = new SimpleType
      {
        IntValue = 46,
        StringValue = ""Hello World"",
        CharValue = '!',
        EnumValue = SimpleEnum.SomeValue
      },
      EmbeddedComplex = new ComplexType
      {
        Name = ""embedded"",
        EmbeddedSimple = new SimpleType
        {
          IntValue = 269,
          StringValue = ""Bye world"",
          CharValue = '~',
          EnumValue = SimpleEnum.SomeOtherValue
        },
        EmbeddedComplex = null
      }
    };";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildArray()
    {
      ArrayType obj = new ArrayType
      {
        Name = "test array",
        Numbers = new int[] { 1, 2, 10 }
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "Array");
      const string expected =
@"    public static readonly ArrayType Array = new ArrayType
    {
      Name = ""test array"",
      Numbers = new int[]
      {
        1,
        2,
        10
      }
    };";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildListNew()
    {
      ListTypeNew obj = new ListTypeNew
      {
        Name = "test list",
        Items = new List<SimpleType>
        {
          new SimpleType { StringValue = "item 1", CharValue = '1'},
          new SimpleType { StringValue = "item 2", CharValue = '2', IntValue = 25 }
        }
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "List"); const string expected =
@"    public static readonly ListTypeNew List = new ListTypeNew
    {
      Name = ""test list"",
      Items = new List<SimpleType>
      {
        new SimpleType
        {
          IntValue = 0,
          StringValue = ""item 1"",
          CharValue = '1',
          EnumValue = SimpleEnum.SomeValue
        },
        new SimpleType
        {
          IntValue = 25,
          StringValue = ""item 2"",
          CharValue = '2',
          EnumValue = SimpleEnum.SomeValue
        }
      }
    };";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildDictionaryNew()
    {
      DictionaryTypeNew obj = new DictionaryTypeNew
      {
        Name = "test list",
        Items = new Dictionary<string, SimpleType>
        {
          { "test1", new SimpleType { StringValue = "item 1", CharValue = '1'} },
          { "test2", new SimpleType { StringValue = "item 2", CharValue = '2', IntValue = 25 } }
        }
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "Dictionary"); const string expected =
@"    public static readonly DictionaryTypeNew Dictionary = new DictionaryTypeNew
    {
      Name = ""test list"",
      Items = new Dictionary<string, SimpleType>
      {
        {
          ""test1"",
          new SimpleType
          {
            IntValue = 0,
            StringValue = ""item 1"",
            CharValue = '1',
            EnumValue = SimpleEnum.SomeValue
          }
        },
        {
          ""test2"",
          new SimpleType
          {
            IntValue = 25,
            StringValue = ""item 2"",
            CharValue = '2',
            EnumValue = SimpleEnum.SomeValue
          }
        }
      }
    };";

      Assert.AreEqual(expected, actual);
    }

    // TODO flag enums
    // TODO non-creatable lists/dictionaries/arrays
    // TODO constructors
  }
}
