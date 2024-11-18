[![GitHub license](https://img.shields.io/badge/license-MIT-brightgreen.svg?style=flat-square)](https://raw.githubusercontent.com/BrokenEvent/Object2Code/refs/heads/master/LICENSE)

# Object2Code

Runtime instance to creation code transformer.

## Usecase

Object2Code is used to generate the object creation C# code of the runtime objects with pre-existing data.
The source object may be configured by user, deserialized or whatever.
By using the code from this repository you can create the code to create such object and full it with current runtime data.

## Usage Example

Use entry point `BrokenEvent.Object2Code.CodeBuilder` and pass the source object to one of its methods.

```csharp
CodeBuilder.BuildStaticReadOnly(obj, "Simple");
```

The string result of such operation will be something like:

```csharp
    public static readonly SimpleType Simple = new SimpleType
    {
      IntValue = 25,
      StringValue = ""Hello World"",
      CharValue = '!',
      EnumValue = SimpleEnum.SomeValue
    };
```

(the code from unit tests).

### Formatting

We use two spaces as indent which is set as the default. This may be changed using `BuildSettings.Indent` as well as the other properties.
