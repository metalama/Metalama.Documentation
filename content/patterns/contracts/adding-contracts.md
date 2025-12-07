---
uid: value-contracts
summary: "The document provides a detailed guide on how to validate input and output values of fields, properties, or parameters using Metalama.Patterns.Contracts package in coding. It covers contract inheritance, primary constructor parameters, return values, out parameters, ref parameters, and fields and properties."
level: 100
keywords: "validate input values, Metalama.Patterns.Contracts, contract inheritance, validate return values, validate out parameters, validate ref parameters, validate output values, validate fields, validate properties, primary constructor parameters"
created-date: 2024-07-22
modified-date: 2025-12-01
---

# Validating field, property, and parameter values

## Validating input values of fields, properties, or parameters

Most often, you add contracts directly to their target field, property, or parameter using custom attributes.

Follow these steps:

1. Add the `Metalama.Patterns.Contracts` package.
2. Add one of the <xref:contract-types?text=contract attributes> to the fields, properties, or parameters you want to validate.

### Example: validating input values

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/Input.cs]

## Validating primary constructor parameters

Contracts can also be applied to primary constructor parameters. This enables validation of constructor arguments, a feature that can't be achieved with standard C# alone.

### Example: contracts on primary constructor parameters

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/PrimaryConstructor.cs]

## Using contract inheritance

By default, all contracts are inherited from interfaces and `virtual` or `abstract` members to their implementation. When you add a contract to an interface member, it's automatically implemented in all classes that implement this interface. The same rule applies to `virtual` or `abstract` members.

### Example: contract inheritance

In the following example, contracts are applied to members of the `ICustomer` interface. You can observe that they're automatically implemented by the `Customer` class that implements the interface.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/Inheritance.cs]

## Validating output values (postconditions)

The most common use of code contracts is to validate the input data flow. This happens by default when you apply a contract to a field, property, or any parameter except `out` ones. When you validate the input data flow, you are essentially being cautious and defensive against the code _calling_ you. This is a best practice as it prevents defects of foreign components from causing unexplainable failures in your own component.

Validating the output data flow can also be useful. This is particularly beneficial when you distrust the _implementation_ of some interface or virtual method. Therefore, it makes more sense to validate the output data flow when the constraint is applied to an interface or virtual member, and inheritance is used to enforce the constraint on implementations.

If the validation of the output data flow fails, an exception of type <xref:Metalama.Patterns.Contracts.PostconditionViolationException> is thrown.

### Return values

To validate the return value of a method, apply the contract to the return parameter using the `[return: XXX]` syntax.

#### Example: contract on return value

In the following example, a `[NotEmpty]` contract has been added to the return value of the `GetCustomerName` method in the `ICustomerService` interface. The `CustomerService` class implements this interface, and you can observe how the return value of the `GetCustomerName` method implementation is validated by the `[NotEmpty]` contract.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/ReturnValue.cs]

### Out parameters

To validate the value of an `out` parameter just before the method exits, simply apply the custom attribute to the parameter as usual.

#### Example: contract on out parameter

In the following example, a `[NotEmpty]` contract has been added to the `out` parameter of the `TryGetCustomerName` method in the `ICustomerService` interface. The `CustomerService` class implements this interface, and you can observe how the value of the `out` parameter of the `TryGetCustomerName` method implementation is validated by the `[NotEmpty]` contract.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/OutParameter.cs]

### Ref parameters

By default, only the input value of `ref` parameters is validated. To change the default behavior, use the <xref:Metalama.Patterns.Contracts.ContractBaseAttribute.Direction> property. To validate only the output value, use the <xref:Metalama.Framework.Aspects.ContractDirection.Output> value. To validate both input and output values, use <xref:Metalama.Framework.Aspects.ContractDirection.Both>.

#### Example: contract on ref parameter

In the following example, a `[Positive]` contract has been added to the `ref` parameter of the `CountWords` method of `IWordCounter`. The <xref:Metalama.Patterns.Contracts.ContractBaseAttribute.Direction> property is set to <xref:Metalama.Framework.Aspects.ContractDirection.Both> so that both the input and output value of the parameter are verified. The `WordCounter` class implements the `IWordCounter` interface. You can observe that the `[Positive]` contract is verified both when the method enters and when it completes.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/RefParameter.cs]

### Fields and properties

At first glance, it may seem surprising, but fields and properties also have an input and output flow if you consider them from the right perspective. The input flow is the _assignment_ one—the value passed to the _setter_—while the output flow is the one of the _getter_.

By default, when a contract is applied to a field or property that has a setter, the contract validates the value passed to the setter.

Just like with `ref` parameters, you can use the <xref:Metalama.Patterns.Contracts.ContractBaseAttribute.Direction> and set it to either <xref:Metalama.Framework.Aspects.ContractDirection.Output> or <xref:Metalama.Framework.Aspects.ContractDirection.Both>.

#### Example: output contracts on properties

In the following example, we've added the `[NotEmpty]` contract to two properties of the `IItem` interface. The `Key` property is get-only, so the contract applies to the getter return value by default. The `Value` property has both a getter and a setter, so we've set the <xref:Metalama.Patterns.Contracts.ContractBaseAttribute.Direction> property to <xref:Metalama.Framework.Aspects.ContractDirection.Both> to validate both the input value and output value.

The `Item` class implements the `IItem` interface. You can observe that the contracts defined on the `IItem` interface are implemented in the code.

In the `Item` class, the `Key` property is implemented as an automatic property. It might seem surprising that the contract is still implemented in the getter instead of the setter. The reason is to preserve the semantics of the contract: when applied to the getter, the contract promises to throw a <xref:Metalama.Patterns.Contracts.PostconditionViolationException> exception upon violation. Implementing the contract on the setter would change the contract. Specifically, no exception would be thrown if the property is never set.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/Property.cs]

> [!div class="see-also"]
> <xref:contract-patterns>
> <xref:contract-types>
> <xref:configuring-contracts>
> <xref:invariants>
