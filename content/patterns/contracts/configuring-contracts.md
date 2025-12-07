---
uid: configuring-contracts
summary: "The document provides guidance on configuring contracts in Metalama, covering both compile-time and run-time settings. It explains how to change validation logic, enable/disable contracts, customize inheritance or contract direction, and customize exception types or messages."
level: 200
keywords: "contracts configuration, contract options, enable/disable contracts, Metalama, ContractHelpers class, ContractOptions class, exception customization, .NET"
created-date: 2024-07-22
modified-date: 2025-11-30
---

# Configuring contracts

There are two types of configuration options: compile-time and run-time. Compile-time options affect code generation and must be configured using a fabric. Run-time settings, on the other hand, must be configured during application startup.

Begin with run-time settings.

## Changing the validation logic

Some contracts use flexible run-time settings for their validation. These settings are modifiable properties of the <xref:Metalama.Patterns.Contracts.ContractHelpers> class.

| Aspect | Property | Description |
|-----|----|-----|
| <xref:Metalama.Patterns.Contracts.CreditCardAttribute> | <xref:Metalama.Patterns.Contracts.ContractHelpers.IsValidCreditCardNumber> | The `Func<string?, bool>` validates the string as a credit card number. |
| <xref:Metalama.Patterns.Contracts.EmailAttribute>  | <xref:Metalama.Patterns.Contracts.ContractHelpers.EmailRegex> | A regular expression validates the string as an email address. |
| <xref:Metalama.Patterns.Contracts.UrlAttribute>  | <xref:Metalama.Patterns.Contracts.ContractHelpers.UrlRegex> | A regular expression validates the string as a URL. |
| <xref:Metalama.Patterns.Contracts.PhoneAttribute>  | <xref:Metalama.Patterns.Contracts.ContractHelpers.PhoneRegex> | A regular expression validates the string as a phone number. |

These properties can be set during the initialization sequence of your application, and any changes will affect the entire application.

There's currently no way to modify these settings for a specific namespace or type within your application.

## Changing compile-time options

All other configurable options are compile-time ones, represented by the <xref:Metalama.Patterns.Contracts.ContractOptions> class. These options can be configured granularly from a fabric using the configuration framework described in <xref:fabrics-configuration>.

### Enabling and disabling contracts

Contracts are often useful during the early phases of development. As the code stabilizes, you can disable them. However, when a problem arises, it may be beneficial to re-enable them for troubleshooting.

The <xref:Metalama.Patterns.Contracts.ContractOptions> class provides three properties that allow you to enable or disable contracts for the entire project, or more specifically for a given namespace or type:

- <xref:Metalama.Patterns.Contracts.ContractOptions.ArePreconditionsEnabled>
- <xref:Metalama.Patterns.Contracts.ContractOptions.ArePostconditionsEnabled>
- <xref:Metalama.Patterns.Contracts.ContractOptions.AreInvariantsEnabled>

These options are enabled by default. When you disable them, the code supporting these features won't be generated.

#### Example: disabling invariants in a namespace

In the example below, we have invariants in two sub-namespaces: `Invoicing` and `Fulfillment`. We use a <xref:Metalama.Framework.Fabrics.ProjectFabric> and the <xref:Metalama.Patterns.Contracts.ContractOptions> to disable invariants for the `Fulfillment` namespace.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/Invariants_Disable.cs]

#### Example: disabling preconditions for a type or method

The following example demonstrates granular control over contract generation. The fabric disables preconditions for the entire `DisabledService` class and also disables them for a specific method (`UnvalidatedMethod`) in `MixedService` while leaving other methods validated.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/DisablePreconditions.cs]

### Changing the default inheritance or contract direction options

By default, contract inheritance is enabled and contract direction is set to <xref:Metalama.Framework.Aspects.ContractDirection.Default>. To change these default values, use the <xref:Metalama.Patterns.Contracts.ContractOptions.IsInheritable> and <xref:Metalama.Patterns.Contracts.ContractOptions.Direction> properties of the <xref:Metalama.Patterns.Contracts.ContractOptions> object.

### Customizing the exception type or text

The default behavior of Metalama Contracts is to generate code that throws the default .NET exception with a hard-coded error message. This default behavior is implemented by the <xref:Metalama.Patterns.Contracts.ContractTemplates> class.

To customize the type of exceptions thrown or the exception messages (for example, to localize them), override the <xref:Metalama.Patterns.Contracts.ContractTemplates> class. Follow these steps:

1. Create a class derived from the <xref:Metalama.Patterns.Contracts.ContractTemplates>.
2. Override any or all templates. You may want to refer to the original source code of the <xref:Metalama.Patterns.Contracts.ContractTemplates> class for inspiration.
3. Using a <xref:Metalama.Framework.Fabrics.ProjectFabric> or a <xref:Metalama.Framework.Fabrics.NamespaceFabric>, set the <xref:Metalama.Patterns.Contracts.ContractOptions.Templates> property of the <xref:Metalama.Patterns.Contracts.ContractOptions> object.

#### Example: translating error messages

The following example demonstrates how to translate the exception messages into French.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/Localize.cs]

> [!div class="see-also"]
> <xref:contract-patterns>
> <xref:value-contracts>
> <xref:fabrics-configuration>
