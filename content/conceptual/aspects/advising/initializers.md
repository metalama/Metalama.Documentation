---
uid: initializers
level: 300
summary: "The document provides instructions on how to add initializers to fields, properties, object constructors, and type constructors using the Metalama Framework. It includes examples for each case."
keywords: "initializers, fields, properties, Metalama Framework, initialization, declarative advice, programmatic advice, constructors, object constructors, type constructors"
created-date: 2023-02-17
modified-date: 2025-11-30
---

# Adding initializers

## Initialization of fields and properties

### Inline initialization of declarative advice

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

### Initialization of programmatic advice

If you use the programmatic advice <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceProperty*>, <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceField*>, or <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceEvent*>, you can set the <xref:Metalama.Framework.Code.DeclarationBuilders.IFieldOrPropertyBuilder.InitializerExpression> in the lambda passed to the `build*` parameter of these advice methods.

#### Example: Initializing a programmatically introduced field

In the following example, the aspect introduces a field using the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceField*> programmatic advice and sets its initializer expression to an array that contains the names of all methods in the target type.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ProgrammaticInitializer.cs name="Programmatic Initializer"]

## Before any object constructor

To inject some initialization before any user code of the instance constructor is called:

1. Add a method of signature `void BeforeInstanceConstructor()` to your aspect class and annotate it with the `[Template]` custom attribute. The name of this method is arbitrary.
2. Call the <xref:Metalama.Framework.Aspects.AdviserExtensions.AddInitializer*?text=builder.Advice.AddInitializer> method in your aspect (or <xref:Metalama.Framework.Aspects.AdviserExtensions.AddInitializer*?text=amender.Advice.AddInitializer> in a fabric). Pass the type that must be initialized, the name of the method from the previous step, and the value `InitializerType.BeforeInstanceConstructor`.

The `AddInitializer` advice will _not_ affect the constructors that call a chained `this` constructor. That is, the advice always runs before any constructor of the current class. However, the initialization logic runs _after_ the call to the `base` constructor if the advised constructor calls the base constructor.

A default constructor will be created automatically if the type doesn't contain any constructor.

### Example: Registering live instances

The following aspect registers any new instance of the target class in a registry of live instances. After an instance has been garbage-collected, it is automatically removed from the registry. The aspect injects the registration logic into the constructor of the target class.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/RegisterInstance.cs name="Register Instance"]

## Before a specific object constructor

If you want to insert logic into a specific constructor, call the <xref:Metalama.Framework.Aspects.AdviserExtensions.AddInitializer*> method and pass an <xref:Metalama.Framework.Code.IConstructor>. With this method overload, you can advise the constructors chained to another constructor of the same type through the `this` keyword.

## Before the type constructor

The same approach can be used to add logic to the type constructor (i.e., static constructor) instead of the object constructor. In this case, the `InitializerType.BeforeTypeConstructor` value should be used.

> [!div class="see-also"]
> <xref:introducing-members>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.AddInitializer*>
> <xref:Metalama.Framework.Aspects.IntroduceAttribute>
