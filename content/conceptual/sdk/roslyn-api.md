---
uid: roslyn-api
level: 400
summary: "The document provides a guide on how to access the syntax tree in the Roslyn API from aspects using the Metalama SDK. It includes steps to reference the SDK and use the Roslyn API."
keywords: "Roslyn API, syntax tree, Metalama SDK, Metalama.Framework.Sdk, Metalama.Framework, ISymbol.DeclaringSyntaxReferences, Metalama.Framework.Engine.CodeModel.SymbolExtensions, IDeclaration, GetSymbol, GetDocumentationCommentId"
---

# Using the Roslyn API from aspects

Aspects typically do not have access to the underlying Roslyn code model of the current project. The most significant consequence of this limitation is that aspects cannot access the syntax tree of method implementation. Indeed, the <xref:Metalama.Framework.Code> namespace only exposes declarations, not implementation syntax.

If your aspect requires access to the syntax tree, it can achieve this by using the Metalama SDK.

## Using the Roslyn API

### Step 1. Reference the Metalama SDK

To use the Roslyn API from an aspect, your project needs to reference the `Metalama.Framework.Sdk` package privately, in addition to the regular `Metalama.Framework` package:

```xml
<PackageReference Include="Metalama.Framework.Sdk" Version="$(MetalamaVersion)" PrivateAssets="all" />
<PackageReference Include="Metalama.Framework" Version="$(MetalamaVersion)" />
```

### Step 2. Use the Roslyn API

Your aspect generally gets access to Roslyn types by using extension methods from the <xref:Metalama.Framework.Engine.CodeModel.SymbolExtensions> class. To get access to syntax, you can then use [ISymbol.DeclaringSyntaxReferences](https://learn.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol.declaringsyntaxreferences).

As an example, you could use Roslyn API to get the documentation comment ID for a Metalama declaration like this:

```c#
static string? GetDocumentationCommentId(this IDeclaration metalamaDeclaration)
{
    var roslynSymbol = metalamaDeclaration.GetSymbol();

    return roslynSymbol.GetDocumentationCommentId();
}
```

