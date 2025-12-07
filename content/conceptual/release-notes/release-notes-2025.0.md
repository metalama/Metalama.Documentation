---
uid: release-notes-2025.0
summary: "Metalama 2025.0 introduces support for .NET 9 and C# 13, improved work with introduced types, T# enhancements, and async WPF commands."
keywords: "Metalama 2025.0, release notes"
created-date: 2024-11-06
modified-date: 2024-11-06
---

# Metalama 2025.0

Metalama 2025.0 focuses on two areas: ensuring compatibility with the latest .NET stack and completing gaps left in the previous version, particularly in support for type introductions. We've also implemented minor improvements requested by the community.

## Support for .NET 9.0 and C# 13

### C# 13 features

Metalama 2025.0 supports all features of C# 13:

- `params` collections
- `ref`/`unsafe` in iterators and async methods
- `ref struct` can implement interfaces
- Classes can have `ref struct` constraints
- New escape character in strings
- Locking on the <xref:System.Threading.Lock> class
- Implicit indexer access in object initializers
- Overload resolution priority
- `partial` properties

### Platform deprecation

- The minimal supported Visual Studio version is now 2022 17.6 LTSC.
- The minimal supported Roslyn version is now 4.4.0.

Third-party package dependencies have been updated.

## Consistent support for source generators and interceptors

Source generators now execute _after_ any Metalama transformation. Previously, code generators executed _before_ Metalama at build time, causing inconsistencies with the design-time experience because Metalama wouldn't "see" the output of source generators.

Aspects can now introduce code that relies on the [GeneratedRegex](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-source-generators) attribute to use build-time-generated regular expressions.

You can also use Roslyn interceptors side-by-side with Metalama since they no longer conflict with code transformations.

## Improved work with introduced types

You can now use introduced types in any type construction. For instance, if `Foo` is your introduced type, you can create a field or parameter of type `Foo<int>`, `Foo[]`, `List<Foo>`, or `Foo*`. This required a major refactoring of our code model.

You can implement generic interfaces bound to a type parameter of the target type. For instance, you can build an `Equatable` aspect that generates code for the `IEquatable<T>` interface, even for introduced types.

## T# improvements

### Dynamic definition of local variables

You can now dynamically define local variables with the new <xref:Metalama.Framework.Aspects.meta.DefineLocalVariable*> method, which offers the following overloads:

```cs
// Explicitly typed
meta.DefineLocalVariable( string nameHint, IType type ) : IExpression
meta.DefineLocalVariable( string nameHint, IType type, dynamic? initializerExpression ) : IExpression
meta.DefineLocalVariable( string nameHint, IType type, IExpresson? initializerExpression ) : IExpression
meta.DefineLocalVariable( string nameHint, Type type ) : IExpression
meta.DefineLocalVariable( string nameHint, Type type, dynamic? initializerExpression ) : IExpression
meta.DefineLocalVariable( string nameHint, Type type, IExpression? initializerExpression ) : IExpression

// var typed
meta.DefineLocalVariable( string nameHint, dynamic? initializerExpression ) : IExpression
meta.DefineLocalVariable( string nameHint, IExpresson? initializerExpression ) : IExpression
```

The `nameHint` parameter suggests the desired local variable name, but the actual name will be chosen dynamically by appending a numerical suffix in case of lexical conflicts with other symbols in the scope.

For details, see the updated <xref:run-time-expressions> article.

### Introduction of static virtual, abstract, and partial members

You can now introduce `static virtual`, `abstract`, and `partial` members thanks to the usual <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceMethod*>, <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceProperty*> and <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceEvent*> methods.

Set the `partial` keyword using the `IMemberBuilder.IsPartial` property.

When introducing a `partial` or `abstract` member, the template's body is ignored. If you don't want to supply a body, mark the template member as `extern` to satisfy the C# compiler.

### Introduction of interfaces

You can now introduce an interface in the same way as you can introduce classes, by using the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceInterface*> method.

### Introduction of extension methods

You can now introduce an extension method by marking the method as `static` and the first parameter as `this`, using either the `[This]` attribute or the `IParameterBuilder.IsThis` property.

### Suppression of well-known irrelevant warnings in aspects

In previous versions, the C# compiler and some analyzers could report irrelevant warnings in aspect code, especially in T# templates. For instance, they could complain that a field is uninitialized or suggest making a method static because they would not see the context in which these template declarations are used.

Metalama 2025.0 now automatically suppresses these warnings, which means that you no longer need to use `#pragma warning disable` in your aspects—or at least less often.

## Async and background WPF commands

The `[Command]` aspect now supports asynchronous commands. The following signatures are now supported for the `Execute` method, with or without a data parameter, with or without a `CancellationToken`.

```csharp
[Command]
Task ExecuteAsync();

[Command]
Task ExecuteAsync( T );

[Command]
Task ExecuteAsync( CancellationToken );

[Command]
Task ExecuteAsync( T, CancellationToken );
```

Asynchronous commands are represented by the <xref:Metalama.Patterns.Wpf.AsyncDelegateCommand> class, which is similar to `CommunityToolkit.Mvvm.Input.AsyncRelayCommand`. It allows you to easily cancel or track the completion of the task.

You can now force the `Execute` method to run in a background thread (instead of the UI thread) by setting the <xref:Metalama.Patterns.Wpf.CommandAttribute.Background> property. This works for both `void` and `Task` methods:

```csharp
[Command( Background = true )]
void Execute();

[Command( Background = true )]
Task ExecuteAsync();
```

Background commands are also represented by an <xref:Metalama.Patterns.Wpf.AsyncDelegateCommand>, even non-`Task` ones.

## Other small improvements

- Test framework: Added test options `@Repeat(<int>)` and `@RandomSeed(<int>)` to help reproduce random issues.
- Code model: `ToDisplayString` and `ToString` implemented for introduced declarations.
- Representation of overridden fields has been made more consistent.
- Some type predicate methods renamed. The old methods have been marked as obsolete.
  - IType.Is -> IsConvertibeTo
  - EligibilityBuilder.MustBe -> MustBeConvertibleTo or MustEqual
  - EligibilityBuilder.MustBeOfType -> MustBeInstanceOfType

## Breaking changes

- The `ReferenceResolutionOptions` enum and all parameters of `ReferenceResolutionOptions` in `IRef.GetTarget` have been removed.
- Casting a non-dynamic expression to <xref:Metalama.Framework.Code.IExpression> no longer works. A call of <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.Capture*> is required instead. The previous behavior "tricking" the cast operator was undocumented and confusing.
- The `IRef.GetTarget` and `IRef.GetTargetOrNull` methods have been moved to extension methods, which could require you to add new `using` directives in your code.
- In `Metalama.Patterns.Wpf`, there are a few changes with the `[Command]` aspect:
  - The <xref:Metalama.Patterns.Wpf.DelegateCommand> type has been moved to the `Metalama.Patterns.Wpf` namespace.
  - The aspect generates properties of type <xref:Metalama.Patterns.Wpf.DelegateCommand>, <xref:Metalama.Patterns.Wpf.DelegateCommand`1>, <xref:Metalama.Patterns.Wpf.AsyncDelegateCommand>, or <xref:Metalama.Patterns.Wpf.AsyncDelegateCommand`1> instead of <xref:System.Windows.Input.ICommand>. All these types implement the <xref:System.Windows.Input.ICommand> interface, but the `Execute(object)` method is now implemented privately. It's replaced by a strongly-typed method `Execute()` for parameterless commands or `Execute(T)` for commands accepting a parameter.

> [!div class="see-also"]
> <xref:release-notes>
