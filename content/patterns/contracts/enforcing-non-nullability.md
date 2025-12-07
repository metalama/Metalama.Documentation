---
uid: enforcing-non-nullability
summary: "The document instructs on using Metalama's VerifyNotNullableDeclarations method to automatically check and enforce non-nullability in C# 8.0, simplifying the process of ensuring clean code."
level: 100
keywords: "non-nullability, C# 8.0, null-checks, Metalama, VerifyNotNullableDeclarations"
created-date: 2024-03-13
modified-date: 2025-11-30
---

# Checking all non-nullable fields, properties and parameters

If you're already using the nullability feature introduced in C# 8.0, you're aware that C# only reports a warning when there's an attempt to assign a `null` value to a non-nullable field, property, or parameter. C# doesn't generate the code that would throw an exception at runtime if this happens. However, if code outside your control consumes your API, it's still a good idea to check all values for `null`.

If you find this task repetitive, frustrating, and unworthy of clean code, we share your sentiment.

Rejoice, solving this problem is a one-liner with Metalama! Simply call the <xref:Metalama.Patterns.Contracts.ContractExtensions.VerifyNotNullableDeclarations*> method from your <xref:Metalama.Framework.Fabrics.ProjectFabric>.

> [!NOTE]
> By default, only your public API is verified. To add checks to your internal API, set the `includeInternalApis` parameter to `true`.

## Example: enforcing all non-nullable fields, properties, and parameters

In the following example, we use the <xref:Metalama.Patterns.Contracts.ContractExtensions.VerifyNotNullableDeclarations*> method to inject `null` checks for our complete public API. Yes, in just one line.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/NotNullFabric.cs]

> [!div class="see-also"]
> <xref:contract-patterns>
> <xref:value-contracts>
> <xref:fabrics>
