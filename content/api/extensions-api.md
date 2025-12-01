---
uid: extensions-api
summary: "The source code for the Extensions API is available on GitHub under the MIT license."
keywords: "Metalama extensions, architecture validation, code fixes, dependency injection, metrics, multicast"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Extensions API

Extensions are additional features built on the <xref:Metalama.Framework> public API. These extensions provide reusable functionality not specific to individual aspects.

| Namespace                             | Description                                                                                                                                                     |
|---------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Extensions.Validation>  | Build aspects that validate code against custom rules. Validate both aspect targets and references to those targets. |
| <xref:Metalama.Extensions.Architecture> | High-level API for enforcing architectural constraints, built on <xref:Metalama.Extensions.Validation>. Define rules using fabrics and verify architecture at compile time. |
| <xref:Metalama.Extensions.CodeFixes>   | Enable aspects to suggest code fixes accessible from the IDE at design time.                                                            |
| <xref:Metalama.Extensions.DependencyInjection>   | Consume dependencies from aspects and transform target code to pull dependencies from dependency injection containers. |
| <xref:Metalama.Extensions.Metrics>   | Implement code metrics based on abstractions defined in <xref:Metalama.Framework.Metrics>. |
| <xref:Metalama.Extensions.Multicast>   | Emulation of PostSharp's `MulticastAttribute` for Metalama, enabling attribute multicasting across code elements. |

> [!div class="see-also"]
> <xref:architecture>
> <xref:dependency-injection-aspect>
