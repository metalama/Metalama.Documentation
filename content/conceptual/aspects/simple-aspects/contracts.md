---
uid: simple-contracts
level: 200
summary: "The document provides a guide on how to use contracts in Metalama for aspect-oriented programming, including creating custom attributes for field, property, or parameter validation, and how to implement the ContractAspect class."
keywords: "aspect-oriented programming, custom attribute, field validation, property validation, parameter validation, Metalama, ContractAspect class, exception handling, value normalization, .NET"
created-date: 2023-03-01
modified-date: 2024-08-04
---

# Getting started: contracts

One of the most prevalent use cases of aspect-oriented programming is the creation of a custom attribute for the validation of fields, properties, or parameters to which it is applied. Examples include `[NotNull]` or `[NotEmpty]`.

In Metalama, this can be achieved by using a _contract_. With a contract, you've the option to:

- Throw an exception when the value doesn't meet a condition of your choosing, or
- Normalize the received value (for instance, by trimming the whitespace of a string).

A contract, technically, is a segment of code that is injected after _receiving_ or before _sending_ a value. It can be utilized for more than just throwing exceptions or normalizing values.


## The simple way: overriding the ContractAspect class

1. Add the `Metalama.Framework` package to your project.

2. Create a new class that derives from the <xref:Metalama.Framework.Aspects.ContractAspect> abstract class. This class will function as a custom attribute, and it is common practice to name it with the `Attribute` suffix.

3. Implement the <xref:Metalama.Framework.Aspects.ContractAspect.Validate*> method in plain C#. This method will act as a <xref:templates?text=template> that defines how the aspect overrides the hand-written target method.

    In this template, the incoming value is represented by the parameter name `value`, irrespective of the actual name of the field or parameter.

    The `nameof(value)` expression will be substituted with the name of the _target_ parameter.

4. The aspect operates as a custom attribute. It can be added to any field, property, or parameter. To validate the return value of a method, use the following syntax: `[return: MyAspect]`.


### Example: null check

The most frequent use of contracts is to verify nullability. Here is the simplest example.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/SimpleNotNull.cs]

Observe how the `nameof(value)` expression is replaced by `nameof(parameter)` when the contract is applied to a parameter.

### Example: trimming

A contract can be used for more than just throwing an exception. In the subsequent example, the aspect trims whitespace from strings. The same aspect is added to properties and parameters.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/trim.cs]

## Going deeper

If you wish to go deeper into contracts, consider referring to the following articles:

- In this article, we've restricted ourselves to very basic contract implementations. To learn how to write more complex code templates, refer to <xref:templates>.
- In this article, we've only applied contracts to the _default direction_ of fields, properties, or parameters. To understand the concept of contract direction, refer to <xref:contracts>.

> [!div class="see-also"]
> <xref:templates>
> <xref:contracts>
> <xref:simple-override-method>

