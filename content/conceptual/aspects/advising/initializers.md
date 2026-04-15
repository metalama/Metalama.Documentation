---
uid: initializers
level: 300
summary: "The document provides instructions on how to add initializers to fields, properties, object constructors, and type constructors using the Metalama Framework. It includes examples for each case."
keywords: "initializers, fields, properties, Metalama Framework, initialization, declarative advice, programmatic advice, constructors, object constructors, type constructors, AfterObjectInitializer, AfterLastInstanceConstructor, IInitializable, records"
created-date: 2023-02-17
modified-date: 2026-04-15
---

# Adding initializers

## Initialization of fields and properties

### Declarative introductions

A simple way to initialize a field or property introduced by an aspect is to add an initializer to the template. For instance, if your aspect introduces a field `int f` and you want to initialize it to `1`, you would write:

 ```cs
 [Introduce]
 int f = 1;
 ```

#### Example: Introducing a Guid property

In the example below, the aspect introduces an `Id` property of type `Guid` and initializes it to a new unique value.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceId.cs name="Introduce Id"]

#### Example: Initializing with a template

The T# template language can also be used within initializers for fields or properties. The aspect in the following example introduces a property that is initialized to the build configuration and target framework.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/BuildInfo.cs name="Introduce Build Info"]

### Programmatic introductions

If you use the programmatic advice <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceProperty*>, <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceField*>, or <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceEvent*>, you can set the <xref:Metalama.Framework.Code.DeclarationBuilders.IFieldOrPropertyBuilder.InitializerExpression> in the lambda passed to the `build*` parameter of these advice methods.

#### Example: Initializing a programmatically introduced field

In the following example, the aspect introduces a field using the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceField*> programmatic advice and sets its initializer expression to an array that contains the names of all methods in the target type.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ProgrammaticInitializer.cs name="Programmatic Initializer"]


## Before the type constructor

To inject logic into the type (static) constructor, use `InitializerKind.BeforeTypeConstructor`. The aspect's template runs once per type (or per generic type instance, in case of generic types) at the first use of that type, after any static field initializers, but before any user code in the existing static constructor, if any.

### Example: Self-registering a generic message handler

The following aspect targets a generic `Handler<TMessage>`. For every closed type the compiler or program constructs, such as `Handler<OrderPlaced>` and `Handler<OrderShipped>`, the generated static constructor registers the pair `(typeof(TMessage), typeof(Handler<TMessage>))` with a static `MessageRouter`. 

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/BeforeTypeConstructor.cs name="Before Type Constructor" diff-files="\.Program\.cs$"]


## Before any object constructor

To inject some initialization before any user code of the instance constructor is called:

1. Add a method of signature `void BeforeInstanceConstructor()` to your aspect class and annotate it with the `[Template]` custom attribute. The name of this method is arbitrary.
2. Call the <xref:Metalama.Framework.Aspects.AdviserExtensions.AddInitializer*?text=builder.Advice.AddInitializer> method in your aspect (or <xref:Metalama.Framework.Aspects.AdviserExtensions.AddInitializer*?text=amender.Advice.AddInitializer> in a fabric). Pass the type that must be initialized, the name of the method from the previous step, and the value `InitializerKind.BeforeInstanceConstructor`.

The `AddInitializer` advice will _not_ affect the constructors that call a chained `this` constructor. That is, the advice always runs before any constructor of the current class. However, the initialization logic runs _after_ the call to the `base` constructor if the advised constructor calls the base constructor.

A default constructor will be created automatically if the type doesn't contain any constructor.

This initializer kind also supports records, including positional records. The initializer code is injected into the primary constructor.

### Example: Registering live instances

The following aspect registers any new instance of the target class in a registry of live instances. After an instance has been garbage-collected, it is automatically removed from the registry. The aspect injects the registration logic into the constructor of the target class.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/RegisterInstance.cs name="Register Instance"]

### Example: Initializing a record

The following example applies `BeforeInstanceConstructor` to a positional record. The primary constructor is materialized into a normal constructor and a set of properties. The initializer code is injected at the beginning of the synthetised constructor.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/RecordInitializer.cs name="Record Initializer"]

## Before a specific object constructor

If you want to insert logic into a specific constructor, call the <xref:Metalama.Framework.Aspects.AdviserExtensions.AddInitializer*> method and pass an <xref:Metalama.Framework.Code.IConstructor>. With this method overload, you can advise the constructors chained to another constructor of the same type through the `this` keyword.

## After the last instance constructor

To inject logic that executes after the whole chain of instance constructors has executed for an object, use `InitializerKind.AfterLastInstanceConstructor`. This is useful when you need to perform actions after the constructor has fully initialized the object, but before the object initializer or the `with` expression sets fields and properties.

1. Add a template method to your aspect class and annotate it with `[Template]`.
2. Call the <xref:Metalama.Framework.Aspects.AdviserExtensions.AddInitializer*> method with the value `InitializerKind.AfterLastInstanceConstructor`.

Metalama introduces an `OnConstructed` helper method on the target type and emits calls to it from every constructor. Constructors that chain to another constructor of the same type using `this(...)` still include the generated call, but duplicate execution is prevented by the <xref:Metalama.Framework.RunTime.Initialization.InitializationContext> and its `IsHandled` check.

For non-sealed types, the introduced method is `protected virtual`, allowing derived types to participate in the initialization chain. An <xref:Metalama.Framework.RunTime.Initialization.InitializationContext> parameter is added to each constructor to coordinate initialization across inheritance hierarchies.

### Example: Publishing a domain event after construction

The following aspect publishes a domain event once an object has been fully constructed. If any constructor throws, the event is not published, so subscribers only see successfully-created instances. Because the generated `OnConstructed` method is `protected virtual`, a derived type such as `RecurringOrder` inherits the initialization chain automatically and its constructor also ends with the call to `OnConstructed`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AfterLastInstanceConstructor.cs name="After Last Instance Constructor" diff-files="\.Program\.cs$"]

## After object initialization

An _object initializer_ is the `{ ... }` block that follows a `new` expression and assigns values to accessible fields or properties, for example `new Document { Id = "doc-1", Title = "Report" }`. The assignments run after the constructor has returned, so any logic placed at the end of the constructor cannot see those values.

To inject logic that runs after the constructor _and_ any object initializer or `with` expression has completed, use `InitializerKind.AfterObjectInitializer`. This is the only reliable way to validate or compute derived state after all properties and fields have been set, including those assigned via object initializers.

1. Add a template method to your aspect class and annotate it with `[Template]`.
2. Call the <xref:Metalama.Framework.Aspects.AdviserExtensions.AddInitializer*> method with the value `InitializerKind.AfterObjectInitializer`.

Metalama makes the target type implement the <xref:Metalama.Framework.RunTime.Initialization.IInitializable> interface, which defines an <xref:Metalama.Framework.RunTime.Initialization.IInitializable.Initialize> method. This method is called automatically after construction and object initialization by the framework's call-site rewriting.

For non-sealed types, the `Initialize` method is `virtual`, allowing derived types to override it and call `base.Initialize(...)` to chain initialization logic.

### Example: Publishing after initialization

The next aspect is a variation of the previous one: it publishes the event after the object initializer has run, so the payload can depend on properties set in the object initializer. The `Id` of a `Document` is only known once the object initializer has assigned it, so the publish cannot happen at the end of the constructor. 

With `AfterObjectInitializer`, the aspect implements <xref:Metalama.Framework.RunTime.Initialization.IInitializable> on the target type, and the framework rewrites _call sites_ (see the Program Code) such as `new Document { Id = "..." }` or the `with` expression to invoke `Initialize` after the `{ ... }` block.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AfterObjectInitializer.cs name="After Object Initializer" diff-files="\.Program\.cs$"]

## Combining hand-written initialization logic with aspect-generated one

When the target type supplies its own `OnConstructed` or `Initialize` method, Metalama merges the aspect's statements into the user's method rather than replacing it. 

### Example: Tracking lifecycle with all three initializer kinds

The following `TrackLifecycle` aspect registers the instance's lifecycle state (`BeingConstructed`, `Constructed`, `Initialized`) in a static registry, using `BeforeInstanceConstructor`, `AfterLastInstanceConstructor`, and `AfterObjectInitializer` respectively. The `Customer` target supplies its own `OnConstructed` (which freezes a mutable tag collection) and `Initialize` (which performs cross-property validation).

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/MixedInitialization.cs name="Mixed Initialization" diff-files="\.Program\.cs$"]

## Ordering of initializers

When multiple aspects add initializers to the same type, the order of initializer statements in the generated code respects `AspectOrderDirection.RunTime`. This means that if you define an aspect ordering using `[assembly: AspectOrder(AspectOrderDirection.RunTime, typeof(FirstAspect), typeof(SecondAspect))]`, the initializer from `FirstAspect` will execute before the initializer from `SecondAspect`.

> [!div class="see-also"]
> <xref:introducing-members>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.AddInitializer*>
> <xref:Metalama.Framework.Aspects.IntroduceAttribute>
> <xref:Metalama.Framework.RunTime.Initialization.IInitializable>
> <xref:Metalama.Framework.RunTime.Initialization.InitializationContext>
