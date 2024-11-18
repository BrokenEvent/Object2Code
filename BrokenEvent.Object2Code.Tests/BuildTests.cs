using System;
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

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "SimpleFullName", new BuilderSettings { UseFullNames = true });
      const string expected =
@"    public static readonly BrokenEvent.Object2Code.Tests.Types.SimpleType SimpleFullName = new BrokenEvent.Object2Code.Tests.Types.SimpleType
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

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "SimpleBraces", new BuilderSettings { SkipBracesForEmptyConstructor = false });
      const string expected =
@"    public static readonly SimpleType SimpleBraces = new SimpleType()
    {
      IntValue = 25,
      StringValue = ""Hello World"",
      CharValue = '!',
      EnumValue = SimpleEnum.SomeValue
    };";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildConstructorHybrid()
    {
      ConstructorTypeReadOnly obj = new ConstructorTypeReadOnly(25)
      {
        StringValue = "Hello World",
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "CtorReadOnly");
      const string expected =
@"    public static readonly ConstructorTypeReadOnly CtorReadOnly = new ConstructorTypeReadOnly(
        25
      )
    {
      StringValue = ""Hello World""
    };";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildConstructorAllReadOnly()
    {
      ConstructorTypeAllReadOnly obj = new ConstructorTypeAllReadOnly(25, "Hello World");

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "CtorAllReadOnly");
      const string expected =
@"    public static readonly ConstructorTypeAllReadOnly CtorAllReadOnly = new ConstructorTypeAllReadOnly(
        25,
        ""Hello World""
      );";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildConstructorHidden()
    {
      ConstructorHidden obj = new ConstructorHidden(25);

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "CtorHidden");
      const string expected =
@"    public static readonly ConstructorHidden CtorHidden = new ConstructorHidden(
        default(int)
      );";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildConstructorHiddenTodo()
    {
      ConstructorHidden obj = new ConstructorHidden(25);

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "CtorHiddenTodo", new BuilderSettings { AddToDo = true });
      const string expected =
@"    public static readonly ConstructorHidden CtorHiddenTodo = new ConstructorHidden(
        default(int) /* TODO */
      );";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildConstructorPrivateTodo()
    {
      ConstructorPrivate obj = ConstructorPrivate.Create(25);

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "CtorPrivateTodo", new BuilderSettings { ThrowOnMissingConstructor = false });
      const string expected =
@"    public static readonly ConstructorPrivate CtorPrivateTodo = new ConstructorPrivate()/* TODO */;";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildConstructorPrivateThrow()
    {
      ConstructorPrivate obj = ConstructorPrivate.Create(25);

      Assert.Throws(
          typeof(InvalidOperationException),
          () => CodeBuilder.BuildStaticReadOnly(obj, "CtorPrivateThrow", new BuilderSettings { ThrowOnMissingConstructor = true })
        );
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

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "Complex");
      const string expected =
@"    public static readonly ComplexType Complex = new ComplexType
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

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "ComplexNoNulls", new BuilderSettings { NullValuesHandling = NullValues.Ignore });
      const string expected =
@"    public static readonly ComplexType ComplexNoNulls = new ComplexType
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

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "MoreComplex");
      const string expected =
@"    public static readonly ComplexType MoreComplex = new ComplexType
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
        }
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

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "ListNew");
      const string expected =
@"    public static readonly ListTypeNew ListNew = new ListTypeNew
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
    public void BuildListExisting()
    {
      ListTypeExisting obj = new ListTypeExisting()
      {
        Name = "test list",
        Items = 
        {
          new SimpleType { StringValue = "item 1", CharValue = '1'},
          new SimpleType { StringValue = "item 2", CharValue = '2', IntValue = 25 }
        }
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "ListExisting");
      const string expected =
@"    public static readonly ListTypeExisting ListExisting = new ListTypeExisting
    {
      Name = ""test list"",
      Items = 
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

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "DictionaryNew"); const string expected =
@"    public static readonly DictionaryTypeNew DictionaryNew = new DictionaryTypeNew
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

    [Test]
    public void BuildDictionaryExisting()
    {
      DictionaryTypeExisting obj = new DictionaryTypeExisting
      {
        Name = "test list",
        Items = 
        {
          { "test1", new SimpleType { StringValue = "item 1", CharValue = '1'} },
          { "test2", new SimpleType { StringValue = "item 2", CharValue = '2', IntValue = 25 } }
        }
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "DictionaryExisting"); const string expected =
@"    public static readonly DictionaryTypeExisting DictionaryExisting = new DictionaryTypeExisting
    {
      Name = ""test list"",
      Items = 
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

    [Test]
    public void BuildFlagsEnum1()
    {
      FlagsEnumType obj = new FlagsEnumType
      {
        Value = FlagsEnum.OneValue
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "FlagsEnum1");
      const string expected =
@"    public static readonly FlagsEnumType FlagsEnum1 = new FlagsEnumType
    {
      Value = FlagsEnum.OneValue
    };";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildFlagsEnum2()
    {
      FlagsEnumType obj = new FlagsEnumType
      {
        Value = FlagsEnum.OneValue | FlagsEnum.AnotherValue
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "FlagsEnum2");
      const string expected =
@"    public static readonly FlagsEnumType FlagsEnum2 = new FlagsEnumType
    {
      Value = FlagsEnum.OneValue | FlagsEnum.AnotherValue
    };";

      Assert.AreEqual(expected, actual);
    }

    [Test]
    public void BuildFlagsEnum0()
    {
      FlagsEnumType obj = new FlagsEnumType
      {
        Value = 0
      };

      string actual = CodeBuilder.BuildStaticReadOnly(obj, "FlagsEnum0");
      const string expected =
@"    public static readonly FlagsEnumType FlagsEnum0 = new FlagsEnumType
    {
      Value = 0
    };";

      Assert.AreEqual(expected, actual);
    }
  }
}
