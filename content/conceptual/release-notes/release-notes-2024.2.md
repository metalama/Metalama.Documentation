---
uid: release-notes-2024.2
summary: ""
keywords: "Metalama 2024.2, release notes"
---

# Metalama 2024.2

Metalama 2024.2 has two focal points. The first is the ability to introduce classes, which closes the biggest gap with Roslyn source generators and finally makes it possible to implement patterns like memento or enum view-model. The second priority is to finalize and document the `Metalama.Patterns.Observability` and `Metalama.Patterns.Wpf` packages.

We had to make dozens of smaller improvements to the framework to reach these objectives, and they will benefit everyone.

Here is a detailed list.

## Generation of classes 

It is now possible to introduce (i.e. generate) whole classes by using the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*?text=AdviserExtensions.IntroduceClass> method. This method returns an <xref:Metalama.Framework.Advising.IAdviser`1>`<INamedType>`, which you can then use to add members to the new type.

For details, see <xref:introducing-types>.

## Generation and overriding of constructors

You can now introduce a constructor into an existing or new type thanks to the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceConstructor*?text=AdviserExtensions.IntroduceConstructor> method. 

You can also override any constructor using a new overload of the <xref:Metalama.Framework.Advising.AdviserExtensions.Override*> method.


## Metalama.Patterns.Observability is Generally Available

The `Metalama.Patterns.Observability` package is now stable and fully supported.

It contains the <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect, which implements the <xref:System.ComponentModel.INotifyPropertyChanged> interface.

The <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect is incredibly advanced and capable.

Where competing solutions stop at automatic properties, our implementation supports:
* explicit properties with references to fields, other properties, and methods,
* child objects, i.e., properties like `string FullName => $"{this.Model.FirstName} {this.Model.LastName}"`,
* references to properties of the base type,
* use of constant static methods with immutable types.

For details, see <xref:observability>.

## Metalama.Patterns.Wpf  is Generally Available

The `Metalama.Patterns.Wpf` package (formerly named `Metalama.Patterns.Xaml`) is now stable and fully supported.

It contains two aspects: <xref:Metalama.Patterns.Wpf.CommandAttribute?text=[Command]> and <xref:Metalama.Patterns.Wpf.DependencyPropertyAttribute?text=[DependencyAttribute]> to reduce the WPF boilerplate code.

For details, see <xref:wpf>.

## New package: Metalama.Patterns.Immutability 

This new package defines a concept of immutable type. Types can be marked as immutable using the <xref:Metalama.Patterns.Immutability.ImmutableAttribute?text=[Immutable]> aspect or the <xref:Metalama.Patterns.Immutability.Configuration.ImmutabilityConfigurationExtensions.ConfigureImmutability*> fabric method. This information is used by the `Metalama.Patterns.Observability` package to infer the mutability of properties.

For details, see <xref:immutability>.


## Improvements in fabrics and IAspectReceiver

* The <xref:Metalama.Framework.Fabrics.IAmender`1.Outbound?text=IAmender.Outbound> property is now redundant and has been marked as `[Obsolete]`. The <xref:Metalama.Framework.Fabrics.IAmender`1> interface now directly derives from `IAspectReceiver<T>` (now <xref:Metalama.Framework.Fabrics.IQuery`1>) instead of exposing it on the <xref:Metalama.Framework.Fabrics.IAmender`1.Outbound> property. The use of the <xref:Metalama.Framework.Aspects.IAspectBuilder`1.Outbound> property is still required for <xref:Metalama.Framework.Aspects.IAspectBuilder`1>.
* New method <xref:Metalama.Framework.Fabrics.IQuery`1.Tag*?text=IQuery.Tag>: adds an arbitrary tag that is carried on and available for all lambdas on the right side of the `Tag` method for new overloads of all (or most) `IAspectReceiver` methods.
* New method <xref:Metalama.Framework.Fabrics.IQuery`1.SelectTypes*?text=IQuery.SelectTypes>: gets all types in the current context (typically namespace, compilation, or current type).
* New method <xref:Metalama.Framework.Fabrics.IQuery`1.SelectTypesDerivedFrom*?text=IQuery.SelectTypesDerivedFrom>: gets all types in the current context derived from a given type.
* New extension methods for `IAspectReceiver<ICompilation>`:
   * <xref:Metalama.Framework.Fabrics.QueryExtensions.SelectReferencedAssembly*> gets a referenced assembly,
   * <xref:Metalama.Framework.Fabrics.QueryExtensions.SelectReflectionType*> gets a type given by `System.Type`.
* Performance improvements:
    * The right side of query operators like `IAspectReceiver.SelectMany()`, `IAspectReceiver.SelectTypes` or `IAspectReceiver.SelectTypesDerivedFrom` now executes concurrently.
    * When a part of a query is used several times (typically by storing the query in a local variable), its result is cached.

## Improvements in Metalama.Extensions.Architecture

* The reference validator feature now has a concept of _validator granularity_ (<xref:Metalama.Extensions.Validation.ReferenceGranularity>), which accepts the values `Compilation`, `Namespace`, `Type`, `Member`, or `ParameterOrAttribute`. The idea is that when a validator is invariant within some level of granularity, then its predicate should only be evaluated once within the declaration at this level of granularity. For instance, if a validator granularity is set to `Namespace`, then _all_ references within that namespace will be either valid or invalid at the same time.

    The _validator granularity_ concept is essential to improve the performance of validators, as references can be validated collectively instead of one by one.
    
    Code written against the previous API will get obsolescence warnings. We suggest porting your validators to the new API and choosing the coarsest possible granularity.
    
    Additionally, the new class <xref:Metalama.Extensions.Architecture.Predicates.ReferenceEndPredicate> serves as a base for predicates that depend on a single end of the reference to validate. This class is a preparation for some future feature, allowing the validation of inbound references instead of just outbound references.

* The API has been improved with fluent <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.And*>, <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.Or*> and <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.Not*> methods.

## Improvements in diagnostic suppressions

Diagnostic suppressions can now be filtered by argument thanks to the new <xref:Metalama.Framework.Diagnostics.SuppressionDefinition.WithFilter*?text=SuppressionDefinition.WithFilter> method.

## Improvements in the test framework

* The default diff interactive tool will now be opened when an aspect test fails (i.e., the expected snapshot is different from the actual one). The feature works with [DiffEngine](https://github.com/VerifyTests/DiffEngine) and integrates with [DiffEngineTray](https://github.com/VerifyTests/DiffEngine/blob/main/docs/tray.md).

## Improvements in the code model

The following changes improve your ability to generate code with Metalama:

* Adding <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.WithType*> and <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.WithNullability*> extension methods for <xref:Metalama.Framework.Code.IType> to override the inferred type or nullability of a captured expression.
* Ability to evaluate a T# template into an <xref:Metalama.Framework.Code.SyntaxBuilders.IStatement> thanks to the <xref:Metalama.Framework.Code.SyntaxBuilders.StatementFactory.FromTemplate*?text=StatementFactory.FromTemplate> method.
* New concept <xref:Metalama.Framework.Code.SyntaxBuilders.IStatementList> to represent an unresolved list of statements. Statement lists can be built from an `IStatement` or `IEnumerable<IStatement>` using the new extension methods <xref:Metalama.Framework.Code.SyntaxBuilders.StatementExtensions.AsList*> and <xref:Metalama.Framework.Code.SyntaxBuilders.StatementExtensions.UnwrapBlock*> or with the new class <xref:Metalama.Framework.Code.SyntaxBuilders.StatementListBuilder>.
* New class <xref:Metalama.Framework.Code.SyntaxBuilders.SwitchStatementBuilder> to dynamically create a `switch` statement (cases can be added programmatically &mdash; only literal case labels are currently supported).

* New method <xref:Metalama.Framework.Code.Invokers.IMethodInvoker.CreateInvokeExpression*> generating an <xref:Metalama.Framework.Code.IExpression> that represents a method invocation. Can be called outside of a template context.

* New interface <xref:Metalama.Framework.Code.Invokers.IConstructorInvoker> with its <xref:Metalama.Framework.Code.Invokers.IConstructorInvoker.Invoke*> and <xref:Metalama.Framework.Code.Invokers.IConstructorInvoker.CreateInvokeExpression*> methods.

## Improvements in code formatting

* The performance of whole-project output code formatting has been improved. Note that code formatting is disabled by default so it should not affect your standard builds, but `LamaDebug` builds should be faster.
* Redundancies in member access expressions are eliminated where applicable (e.g., `this.X` or `MyType.Y` becomes `X` or `Y`).
* Non-extension calls to extension methods in templates are transformed into extension calls. This is useful because extension methods cannot be called on dynamic types.
* The discard parameter `_`, when used in templates, was renamed to `__1`, `__2` and so on.

## Improvements in advising and code templates

* Added support for lambda statements and anonymous methods of known scope, i.e., either run-time or compile-time (the scope can be coerced using `meta.RunTime` or `meta.CompileTime` when it is not obvious from the context). Lambda expressions returning `dynamic` are not supported and won't be. Single-statement lambdas (e.g., `() => { return 0; }`) are transparently simplified into expression lambdas (e.g., `() => 0`).
* New concept of <xref:Metalama.Framework.Utilities.Promise`1> (with its interface <xref:Metalama.Framework.Utilities.IPromise`1>) to represent results that are not available yet. This mechanism allows resolving chicken-or-egg issues when introducing members when a template must receive a reference to a declaration that has not been introduced yet. A `Promise<T>` can be passed as an argument to a template, which receives it on a parameter of type `T`.
* An error will be reported when attempting to use some template-only methods from a method that is not a template.

## Changes in interface implementation

* The <xref:Metalama.Framework.Advising.AdviserExtensions.ImplementInterface*> advice no longer verifies if all interface members are present. Errors will appear during compilation. Interface members can be introduced using `[InterfaceMember]` as before, but also using `[Introduce]`, or programmatically using `AdviserExtensions.IntroduceMethod`.
* The <xref:Metalama.Framework.Advising.IImplementInterfaceAdviceResult> interface now has a <xref:Metalama.Framework.Advising.IImplementInterfaceAdviceResult.ExplicitMembers> property of type `IAdviser<INamedType>`, which allows introducing explicit (private) members.

## Improvements in Metalama.Patterns.Contracts

We are finally addressing the problem where the <xref:Metalama.Patterns.Contracts.PositiveAttribute?text=[Positive]>, <xref:Metalama.Patterns.Contracts.NegativeAttribute?text=[Negative]>, <xref:Metalama.Patterns.Contracts.LessThanAttribute?text=[LessThan]> and <xref:Metalama.Patterns.Contracts.GreaterThanAttribute?text=[GreaterThan]> aspects had a non-standard behavior because they behave as if the inequality were _unstrict_ while the standard interpretation is _strict_. This mistake was performed in PostSharp back in 2013 and dragged until now for backward compatibility reasons, but we eventually decided to address it.

Starting from Metalama 2024.2, using any of the <xref:Metalama.Patterns.Contracts.PositiveAttribute?text=[Positive]>, <xref:Metalama.Patterns.Contracts.NegativeAttribute?text=[Negative]>, <xref:Metalama.Patterns.Contracts.LessThanAttribute?text=[LessThan]> or <xref:Metalama.Patterns.Contracts.GreaterThanAttribute?text=[GreaterThan]> attributes will report a warning saying that the strictness of the inequality is ambiguous. You have two options to resolve the warning:

* Use one of the variants where the strictness is made explicit:
   * Strict: <xref:Metalama.Patterns.Contracts.StrictlyPositiveAttribute?text=[StrictlyPositive]>, <xref:Metalama.Patterns.Contracts.StrictlyNegativeAttribute?text=[StrictlyNegative]>, <xref:Metalama.Patterns.Contracts.StrictlyLessThanAttribute?text=[StrictlyLessThan]> and <xref:Metalama.Patterns.Contracts.StrictlyGreaterThanAttribute?text=[StrictlyGreaterThan]>
   * Non-strict: <xref:Metalama.Patterns.Contracts.NonNegativeAttribute?text=[NonNegative]>, <xref:Metalama.Patterns.Contracts.NonPositiveAttribute?text=[NonPositive]>, <xref:Metalama.Patterns.Contracts.LessThanOrEqualAttribute?text=[LessThanOrEqual]> and <xref:Metalama.Patterns.Contracts.GreaterThanAttribute?text=[GreaterThanOrEqual]>.
* Or set the <xref:Metalama.Patterns.Contracts.ContractOptions.DefaultInequalityStrictness> contract option using the <xref:Metalama.Patterns.Contracts.ContractConfigurationExtensions.ConfigureContracts*> fabric extension method.

If you don't address the warning, the behavior of the ambiguous contracts will remain backward-compatible, i.e., non-standard.

We will change the default behavior and the warning in a future release.


## Improvements in Metalama.Patterns.Caching

* The Redis backend now consumes the `IConnectionMultiplexer` from the `IServiceProvider` by default, which makes it easier to use with .NET Aspire.

* The cache key prefix now always includes the version of the serialization protocol.

* `CancellationToken` parameters are automatically ignored.

* With the Redis back-end:
    * By default, Metalama Caching will use the `CommandFlags.PreferReplica` for read operations and `CommandFlags.PreferMaster` for write operations. These default values can be modified thanks to the `ReadCommandFlags` and `WriteCommandFlags` properties of the `RedisCachingBackendConfiguration` class.
    * `StackExchange.Redis` was updated to 2.8.
    * The <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingFactory.Redis*> mehtod now consumes `IConnectionMultiplexer` from the `IServiceProvider` by default.


## Improvements in supportability

When troubleshooting Metalama, it is now possible to enable tracing and direct it to the standard output just using an environment variable.

For details, see <xref:creating-logs>.

## Breaking Changes

* The <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicate> class has a new abstract property <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicate>. Its constructor now requires a <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateBuilder>.
* <xref:Metalama.Extensions.Validation.ReferenceValidationContext> no longer reports several <xref:Metalama.Framework.Code.ReferenceKinds>, but only the deepest one. For instance, in `class A : List<C>;`, the reference to `C` is of kind `GenericArgument` and no longer `BaseType | GenericArgument`. Combined flags added complexity, and we did not see a use case for them.
* Projects that were using transitive reference validators (or architecture constraints), if they were built with a previous version of Metalama, must be rebuilt.
* Relationships specified with <xref:Metalama.Framework.Aspects.AspectOrderAttribute> are now applied to derived aspect classes by default. To revert to the previous behavior, set the <xref:Metalama.Framework.Aspects.AspectOrderAttribute.ApplyToDerivedTypes> property to `false`.
* An error will be reported when attempting to use some compile-time methods (for instance, `meta.CompileTime`) from a method that is not a template. In prior versions, these methods had no effect and were only confusing.
* `Metalama.Patterns.Contracts`: Some virtual methods of the <xref:Metalama.Patterns.Contracts.RangeAttribute> and <xref:Metalama.Patterns.Contracts.ContractTemplates> classes have changed; overrides must be adapted.
* `Metalama.Patterns.Caching`:
    * `CachingBackend.Clear()` no longer raises the `ItemRemoved` event.
    * The `CacheValue` class has been replaced by the existing `CacheItem` class.
    * The `ICachingSerializer`interface has been refactored to work with `BinaryReader` and `BinaryWriter`.
