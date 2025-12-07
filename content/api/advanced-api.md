---
uid: advanced-api
summary: "Namespaces and assemblies for extending Metalama with the Roslyn API, including aspect weavers, code model integration, and source transformers."
keywords: "Metalama SDK, Roslyn API, aspect weavers, source transformers, IPartialCompilation, IAspectWeaver, code model"
created-date: 2023-01-26
modified-date: 2025-11-30
level: 400
---

# Advanced extensibility API (SDK)

The `Metalama.Framework.Sdk` package enables extending Metalama using the Roslyn API. These APIs provide low-level access to compilation transformations and are intended for advanced scenarios where the standard advice API is insufficient.

For conceptual guidance on using these APIs, see <xref:sdk>.

## Namespace reference

| Namespace | Description |
|-----------|-------------|
| <xref:Metalama.Compiler> | Provides the lowest-level API for writing source transformers. Implementations of <xref:Metalama.Compiler.ISourceTransformer> can modify the Roslyn compilation directly without any aspect concepts. Use this when you need to transform code independently of the Metalama aspect pipeline. |
| <xref:Metalama.Framework.Engine.AspectWeavers> | Enables implementing Metalama aspects at the Roslyn level using the <xref:Metalama.Framework.Engine.AspectWeavers.IAspectWeaver> interface. Unlike <xref:Metalama.Compiler>, aspect weavers integrate with the Metalama Framework pipeline and have access to aspect instances and the Metalama code model. For step-by-step guidance, see <xref:aspect-weavers>. |
| <xref:Metalama.Framework.Engine.CodeModel> | Bridges the Metalama code model (<xref:Metalama.Framework.Code.IDeclaration>, <xref:Metalama.Framework.Code.IType>) with Roslyn symbols (<xref:Microsoft.CodeAnalysis.ISymbol>). The <xref:Metalama.Framework.Engine.CodeModel.SymbolExtensions> class provides extension methods such as `GetSymbol` and `GetDeclaration` for converting between the two models. The <xref:Metalama.Framework.Engine.CodeModel.IPartialCompilation> interface represents a compilation that can be transformed by aspect weavers. For usage examples, see <xref:roslyn-api>. |
| <xref:Metalama.Framework.Engine.Collections> | Contains specialized collection interfaces and types used internally by the SDK, including <xref:Metalama.Framework.Engine.Collections.IReadOnlyMultiValueDictionary`2>. |
| <xref:Metalama.Framework.Engine.Formatting> | Contains annotations and extension methods for marking generated code in syntax trees. When implementing an <xref:Metalama.Framework.Engine.AspectWeavers.IAspectWeaver>, use the <xref:Metalama.Framework.Engine.Formatting.FormattingAnnotations> class to annotate generated syntax nodes so Metalama can format them correctly. |

> [!div class="see-also"]
> <xref:sdk>
> <xref:aspect-weavers>
> <xref:roslyn-api>
> <xref:custom-metrics>
