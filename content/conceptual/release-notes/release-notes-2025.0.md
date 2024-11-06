---
uid: release-notes-2025.0
summary: ""
keywords: "Metalama 2025.0, release notes"
created-date: 2024-11-06
modified-date: 2024-11-06
---

# Metalama 2025.0

We've focused on two areas: first, ensuring Metalama is compatible with the latest .NET stack, and second, completing gaps left in the previous version, particularly in support for type introductions. We've also implemented minor improvements requested by the community.

We published the following preview builds: [2025.0.1-preview](https://github.com/orgs/postsharp/discussions/371), [2025.0.2-preview](https://github.com/orgs/postsharp/discussions/374), [2025.0.3-preview](https://github.com/orgs/postsharp/discussions/376), and [2025.0.4-preview](https://github.com/orgs/postsharp/discussions/378).

## Support for .NET 9.0 and C# 13

### C# 13 features

We tested and fixed Metalama 2025.0 for all features of C# 13:

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

* The minimal supported Visual Studio version is now 2022 17.6 LTSC.
* The minimal supported Roslyn version is now 4.4.0.

Third-party package dependencies have been updated.

## Consistent support for source generators and interceptors

We now consistently execute source generators _after_ any Metalama transformation. Previously, code generators were executed _before_ Metalama at build time, causing inconsistencies with the design-time experience, as Metalama would not "see" the output of source generators.

The benefit for you is that aspects can introduce code that relies on the [GeneratedRegex](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-source-generators) attribute to use build-time-generated regular expressions.

The second benefit is that you can now use Roslyn interceptors side-by-side with Metalama since they no longer conflict with our code transformations.

## Improved work with introduced types

You can now use introduced types in any type construction. For instance, if `Foo` is your introduced type, you can create a field or parameter of type `Foo<int>`, `Foo[]`, `List<Foo>`, or `Foo*`. This required a major refactoring of our code model.

You can also implement generic interfaces bound to a type parameter of the target type. For instance, you can now build an `Equatable` aspect that generates code for the `IEquatable<T>` interface, even for introduced types.

## T# improvements

### Dynamic definition of local variables

It's now possible to dynamically define local variables with the new <xref:Metalama.Framework.Aspects.meta.DefineLocalVariable*> method, which offers the following overloads:

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

For details, see the updated <xref:template-dynamic-code> article.

### Introduction of static virtual, abstract, and partial members

You can now introduce `static virtual`, `abstract`, and `partial` members thanks to the usual <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceMethod*>, <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceProperty*> and <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceEvent*> methods.

The `partial` keyword can be set using the `IMemberBuilder.IsPartial` property.

When introducing a `partial` or `abstract` member, the template's body is ignored. If you don't want to supply a body altogether, you can mark the template member as `extern`, which will make the C# compiler happy about your template being unimplemented.

### Introduction of interfaces

You can now introduce an interface in the same way as you can introduce classes, by using the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceInterface*> method.

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

* Test framework: Added test options `@Repeat(<int>)` and `@RandomSeed(<int>)` to help reproduce random issues.
* Code model: `ToDisplayString` and `ToString` implemented for introduced declarations.
* Representation of overridden fields has been made more consistent.
* Some type predicate methods renamed. The old methods have been marked as obsolete.
  * IType.Is -> IsConvertibeTo
  * EligibilityBuilder.MustBe -> MustBeConvertibleTo or MustEqual
  * EligibilityBuilder.MustBeOfType -> MustBeInstanceOfType

## Breaking changes

* The `ReferenceResolutionOptions` enum and all parameters of `ReferenceResolutionOptions` in `IRef.GetTarget` have been removed.
* Casting a non-dynamic expression to <xref:Metalama.Framework.Code.IExpression> no longer works. A call of <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.Capture*> is required instead. The previous behavior "tricking" the cast operator was undocumented and confusing.
* The `IRef.GetTarget` and `IRef.GetTargetOrNull` methods have been moved to extension methods, which could require you to add new `using` directives in your code.
* In `Metalama.Patterns.Wpf`, there are a few changes with the `[Command]` aspect:
  * the <xref:Metalama.Patterns.Wpf.DelegateCommand> type has been moved to the `Metalama.Patterns.Wpf` namespace,
  * the aspect generates properties of type <xref:Metalama.Patterns.Wpf.DelegateCommand>, <xref:Metalama.Patterns.Wpf.DelegateCommand`1>, <xref:Metalama.Patterns.Wpf.AsyncDelegateCommand> or <xref:Metalama.Patterns.Wpf.AsyncDelegateCommand`1> instead of <xref:System.Windows.Input.ICommand>. All these types implement the <xref:System.Windows.Input.ICommand> interface, but the `Execute(object)` method is now implemented privately. It is replaced by a strongly-typed method `Execute()` for parameterless commands or `Execute(T)` for commands accepting a parameter.

