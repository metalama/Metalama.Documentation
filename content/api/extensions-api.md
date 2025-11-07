---
uid: extensions-api
summary: "The source code for the Extensions API is available on GitHub under the MIT license."
created-date: 2023-01-26
modified-date: 2023-12-11
---

# Extensions API

Extensions are additional features that are built on the <xref:Metalama.Framework> public API and are not specific to an aspect.

| Namespace                             | Description                                                                                                                                                     |
|---------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Extensions.Validation>  | Enables you to build aspects that can validate user code against your own rules. You can validate both the target of the aspect and _references_ to that target. |
| <xref:Metalama.Extensions.Architecture> | A high-level compile-time API, built on <xref:Metalama.Extensions.Validation>, that allows you to enforce your architecture. |
| <xref:Metalama.Extensions.CodeFixes>   | Allows your aspects to suggest code fixes, accessible at design time from the IDE.                                                            |
| <xref:Metalama.Extensions.DependencyInjection>   | Defines and implements concepts that allow you to consume dependencies from aspects and transform the target code to pull the dependencies. |
| <xref:Metalama.Extensions.Metrics>   | Implements code metrics based on the abstractions defined in <xref:Metalama.Framework.Metrics>. |
| <xref:Metalama.Extensions.Multicast>   | An emulation of PostSharp's `MulticastAttribute` for Metalama. |
