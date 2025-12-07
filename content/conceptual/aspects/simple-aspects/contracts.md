---
uid: simple-contracts
level: 200
summary: "The document provides a guide on how to use contracts in Metalama for aspect-oriented programming, including creating custom attributes for field, property, or parameter validation, and how to implement the ContractAspect class."
keywords: "aspect-oriented programming, custom attribute, field validation, property validation, parameter validation, Metalama, ContractAspect class, exception handling, value normalization, .NET"
created-date: 2023-03-01
modified-date: 2025-11-30
---

# Getting started with contracts

One of the most common use cases of aspect-oriented programming is creating a custom attribute for validating fields, properties, or parameters. Examples include `[NotNull]` or `[NotEmpty]`.

In Metalama, you can achieve this using a _contract_. With a contract, you can:

- Throw an exception when the value doesn't meet a condition of your choosing, or
- Normalize the received value (for instance, by trimming the whitespace of a string).

A contract is a segment of code injected after _receiving_ or before _sending_ a value. You can use it for more than just throwing exceptions or normalizing values.

## The simple way: overriding the ContractAspect class

1. Add the [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) package to your project.

2. Create a new class that derives from the <xref:Metalama.Framework.Aspects.ContractAspect> abstract class. The class functions as a custom attribute, so name it with the `Attribute` suffix.

3. Implement the <xref:Metalama.Framework.Aspects.ContractAspect.Validate*> method in plain C#. This method acts as a <xref:templates?text=template> that defines how the aspect overrides the hand-written target method.

    In this template, the incoming value is represented by the parameter name `value`, regardless of the actual name of the field or parameter.

    The `nameof(value)` expression is substituted with the name of the _target_ parameter.

4. The aspect operates as a custom attribute. Add it to any field, property, or parameter. To validate the return value of a method, use the following syntax: `[return: MyAspect]`.

### Example: null check

The most common use of contracts is to verify nullability. Here's the simplest example.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/SimpleNotNull.cs]

Notice how the `nameof(value)` expression is replaced by `nameof(parameter)` when the contract is applied to a parameter.

### Example: trimming

You can use contracts for more than just throwing exceptions. In the following example, the aspect trims whitespace from strings. The same aspect is added to properties and parameters.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/trim.cs]

## Going deeper

To go deeper into contracts, refer to the following articles:

- This article covers basic contract implementations. To learn how to write more complex code templates, see <xref:templates>.
- This article only applies contracts to the _default direction_ of fields, properties, or parameters. To understand the concept of contract direction, see <xref:contracts>.

> [!div class="see-also"]
> <xref:templates>
> <xref:contracts>
> <xref:simple-override-method>
> <xref:simple-aspects>
> <xref:Metalama.Framework.Aspects.ContractAspect>
> <xref:Metalama.Framework.Aspects.ContractDirection>
> <xref:Metalama.Framework.Code.IFieldOrPropertyOrIndexer>
