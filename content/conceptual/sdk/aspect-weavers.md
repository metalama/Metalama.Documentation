---
uid: aspect-weavers
level: 400
summary: "The document provides a detailed guide on creating weaver-based aspects using the Metalama SDK. It covers referencing the SDK, defining the aspect's public interface, creating the weaver, binding the aspect to the weaver, defining eligibility, implementing the TransformAsync method, and writing unit tests."
keywords: "weaver-based aspects, Metalama SDK, TransformAsync method, Roslyn API, C# code transformations, IAspectWeaver interface, aspect weaver"
created-date: 2023-02-20
modified-date: 2025-11-30
---

# Aspect weavers

Normal aspects are implemented by the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method, which provides advice using the advice factory exposed by the <xref:Metalama.Framework.Aspects.IAspectBuilder> interface. Normal aspects are limited to the capabilities of the <xref:Metalama.Framework.Advising.IAdviceFactory> interface.

Aspect weavers let you perform arbitrary transformations on C# code using the low-level Roslyn API.

When you assign an aspect weaver to an aspect class, Metalama bypasses the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method and instead calls the aspect weaver.

> [!WARNING]
> The use of weaver-based aspects is discouraged:
>
> * They're significantly more complex to implement and integrate less well with IDEs.
> * They have a significant performance impact, especially when many are in use.

## Creating a weaver-based aspect

The following steps guide you through creating a weaver-based aspect and its weaver:

### Step 1. Reference the Metalama SDK

A weaver project needs to reference the `Metalama.Framework.Sdk` package privately, in addition to the regular `Metalama.Framework` package:

```xml
<PackageReference Include="Metalama.Framework.Sdk" Version="$(MetalamaVersion)" PrivateAssets="all" />
<PackageReference Include="Metalama.Framework" Version="$(MetalamaVersion)" />
```

### Step 2. Define the public interface of your aspect (typically an attribute)

Define an aspect class as usual. For example:

```csharp
using Metalama.Framework.Aspects;

namespace Metalama.Community.Virtuosity;

public class VirtualizeAttribute : TypeAspect { }
```

### Step 3. Create the weaver for this aspect

1. Add a class that implements the <xref:Metalama.Framework.Engine.AspectWeavers.IAspectWeaver> interface.
2. Add the <xref:Metalama.Framework.Engine.MetalamaPlugInAttribute> attribute to this class.

At this point, the code should look like this:

```cs
using Metalama.Framework.Engine;
using Metalama.Framework.Engine.AspectWeavers;

namespace Metalama.Community.Virtuosity.Weaver;

[MetalamaPlugIn]
class VirtuosityWeaver : IAspectWeaver
{
    public Task TransformAsync( AspectWeaverContext context )
    {
        throw new NotImplementedException();
    }
}
```

### Step 4. Bind the aspect class to its weaver class

Return to the aspect class and annotate it with a custom attribute of type <xref:Metalama.Framework.Aspects.RequireAspectWeaverAttribute>. The constructor argument must point to the weaver class.

```cs
[RequireAspectWeaver( typeof(VirtuosityWeaver) )]
public class VirtualizeAttribute : TypeAspect { }
```

### Step 5. Define eligibility (optional)

While the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method is ignored for weaver aspects, the <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> method is still called. You can define eligibility in the aspect class as usual (see <xref:eligibility>). For example:

[!code-csharp[](~/source-dependencies/Metalama.Community/src/Metalama.Community.Virtuosity/Metalama.Community.Virtuosity/VirtualizeAttribute.cs)]

### Step 6. Implement the TransformAsync method

<xref:Metalama.Framework.Engine.AspectWeavers.IAspectWeaver.TransformAsync*> has a parameter of type <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext>. This type contains methods for convenient manipulation of the input compilation: <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.RewriteAspectTargetsAsync*> and <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.RewriteSyntaxTreesAsync*>.

Both methods apply a <xref:Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter> to the input compilation. The difference is that <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.RewriteAspectTargetsAsync*> only calls the `Visit` method on declarations that have the aspect attribute, whereas <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.RewriteSyntaxTreesAsync*> lets you modify anything in the entire compilation, but requires more work to identify the relevant declarations.

All methods that apply a <xref:Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter> operate in parallel, which means your implementation must be thread-safe.

For advanced cases, the <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.Compilation> property exposes the input compilation. Your implementation can set this property to the new compilation. The <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.Compilation> property type is the immutable interface <xref:Metalama.Framework.Engine.CodeModel.IPartialCompilation>. This interface and the extension class <xref:Metalama.Framework.Engine.CodeModel.PartialCompilationExtensions> offer different methods to transform the compilation. For instance, <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.RewriteSyntaxTreesAsync*> applies a <xref:Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter> to the input compilation and returns the resulting compilation.

For full control, use <xref:Metalama.Framework.Engine.CodeModel.IPartialCompilation.WithSyntaxTreeTransformations*>.

Remember to write back the <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.Compilation?text=context.Compilation> property when accessing it directly.

Each weaver is invoked once per project, regardless of the number of aspect instances in the project.

The <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.AspectInstances?text=context.AspectInstances> property gives the list of aspect instances your weaver needs to handle.

To map the Metalama code model to an [ISymbol](https://learn.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol), use the extension methods in <xref:Metalama.Framework.Engine.CodeModel.SymbolExtensions>.

Your weaver doesn't need to format the output code. Metalama handles this at the end of the pipeline. However, your weaver must annotate syntax nodes with the annotations declared in the <xref:Metalama.Framework.Engine.Formatting.FormattingAnnotations> class.

#### Example

A simplified version of `VirtuosityWeaver` could look like this:

[!code-csharp[](~/code/Metalama.Documentation.SampleCode.Sdk/VirtuosityWeaver.cs)]

The actual implementation is available [on the GitHub repo](https://github.com/metalama/Metalama.Community/blob/HEAD/src/Metalama.Community.Virtuosity/Metalama.Community.Virtuosity/VirtuosityWeaver.cs).

### Step 7. Write unit tests

You can test your weaver-based aspect like any other aspect (see <xref:aspect-testing>).

## Examples

Examples of `Metalama.Framework.Sdk` weavers:

* [Metalama.Community.Virtuosity](https://github.com/metalama/Metalama.Community/tree/HEAD/src/Metalama.Community.Virtuosity): Makes all possible methods in a type `virtual`
* [Metalama.Community.AutoCancellationToken](https://github.com/metalama/Metalama.Community/tree/HEAD/src/Metalama.Community.AutoCancellationToken): Automatically propagates `CancellationToken` parameters
* [Metalama.Community.Costura](https://github.com/metalama/Metalama.Community/tree/HEAD/src/Metalama.Community.Costura): Bundles .NET Framework applications into a single executable file

> [!div class="see-also"]
>
> **See also**
>
> * <xref:sdk>
> * <xref:roslyn-api>
> * <xref:aspect-testing>
> * <xref:eligibility>
