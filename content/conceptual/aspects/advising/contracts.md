---
uid: contracts
level: 300
summary: "The document provides advanced information on validating parameter, field, and property values with contracts in Metalama Framework. It covers accessing metadata, contract directions, and adding contract advice programmatically."
keywords: "validating parameter values, Metalama Framework, contract directions, adding contract advice, .NET, ContractAspect class, input parameters, output parameters, precondition checks"
created-date: 2023-02-16
modified-date: 2025-12-06
---

# Validating parameter, field, and property values with contracts

In <xref:simple-contracts>, you learned how to create simple contracts by implementing the <xref:Metalama.Framework.Aspects.ContractAspect> class.

This article covers more advanced scenarios.

## Accessing the metadata of the field, property, or parameter being validated

You can access the context of your template code using the following meta APIs:

- `meta.Target.Declaration` returns the target parameter, field, or property as an <xref:Metalama.Framework.Code.IDeclaration>.
- `meta.Target.Expression` returns the target parameter, field, or property as an <xref:Metalama.Framework.Code.IExpression>. Use this to access the type of the target via `meta.Target.Expression.Type`.
- `meta.Target.FieldOrProperty` returns the target field or property. However, it throws an exception if the contract is applied to a parameter.
- `meta.Target.Parameter` returns the parameter (including the parameter representing the return value). It throws an exception if the contract is applied to a field or property.
- `meta.Target.ContractDirection` returns `Input` or `Output` according to the data flow being validated ([see below](#contract-directions)). Typically, it's `Input` for input parameters and property setters, and `Output` for output parameters and return values.

## Contract directions

By default, the <xref:Metalama.Framework.Aspects.ContractAspect> aspect applies the contract to the _default data flow_ of the target parameter, field, or property.

The default direction is as follows:

- For input and `ref` parameters: the _input_ value.
- For fields and properties: the _assigned_ value (i.e., the `value` parameter of the setter).
- For `out` parameters and return value parameters: the _output_ value.

To change the contract direction, override the <xref:Metalama.Framework.Aspects.ContractAspect.GetDefinedDirection*> method of the <xref:Metalama.Framework.Aspects.ContractAspect> class.

For information on customizing eligibility for different contract directions than the default one, see the remarks in the documentation of the <xref:Metalama.Framework.Aspects.ContractAspect> class. To learn about eligibility, visit <xref:eligibility>.

> [!NOTE]
> Prior to Metalama 2023.4, the <xref:Metalama.Framework.Aspects.ContractAspect.GetDefinedDirection*> method didn't exist. Instead, implementations could specify the contract direction in the <xref:Metalama.Framework.Aspects.ContractAspect> constructor or by setting a property named `Direction`. Both the property and the constructor are now obsolete.

### Example: NotNull for output parameters and return values

We previously encountered this aspect in <xref:simple-contracts>. This example refines the behavior: for the _input_ data flow, an `ArgumentNullException` is thrown, but for the output flow, a `PostConditionFailedException` is thrown. Notice how we apply the aspect to `out` parameters and to return values.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/NotNull.cs name="NotNull"]

## Adding contract advice programmatically

Like any advice, you can add a contract to a parameter, field, or property from your aspect's `BuildAspect` method using the <xref:Metalama.Framework.Aspects.AdviserExtensions.AddContract*> method.

> [!NOTE]
> When possible, provide all contracts to the same method from a single aspect. This approach yields better compile-time performance than using several aspects.

### Example: Automatic NotNull

The following snippet demonstrates how to automatically add precondition checks for all situations in the public API where a non-nullable parameter could receive a null value from a consumer.

The [fabric](xref:fabrics) adds a method-level aspect to all exposed methods. Then, the aspect adds individual contracts using the <xref:Metalama.Framework.Aspects.AdviserExtensions.AddContract*> method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/NotNullFabric.cs name="NotNull Fabric"]

> [!NOTE]
> For a production-ready version of this use case, see <xref:enforcing-non-nullability>.

> [!div class="see-also"]
> <xref:simple-contracts>
> <xref:Metalama.Framework.Aspects.ContractAspect>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.AddContract*>
> <xref:contract-types>
