---
uid: architecture
level: 300
summary: "This article describes the architecture of Metalama, showing how aspects, fabrics, advice, diagnostics, and extension packages interact."
keywords: "architecture, aspects, advice, fabrics, diagnostics, transformations, extensibility, validators, code fixes"
created-date: 2023-07-11
modified-date: 2025-12-08
---
# Metalama architecture

Metalama's architecture consists of a core framework and extension packages that plug into it. This article provides a high-level overview of how these components interact.

## Core components

The following diagram illustrates the core Metalama components:

```mermaid
flowchart TB
    SourceCode[Source Code] -- annotated with<br>custom attributes --> Aspects
    SourceCode -- contains --> Fabrics
    Fabrics -- add --> Aspects
    Aspects -- report & suppress --> Diagnostics
    Aspects -- provide --> Advice
    Advice -- provide --> Transformations[Code Transformations]
```

**Aspects** are the primary building blocks of Metalama. They're applied to your source code through custom attributes or programmatically via fabrics, and can:

- Report and suppress diagnostics
- Provide advice that generates code transformations

**Fabrics** provide a way to apply aspects programmatically. Unlike attribute-based aspects, fabrics exist within your source code and select which declarations receive aspects.

**Advice** specifies the code transformations to apply. Aspects provide advice, which Metalama uses to generate the actual code transformations.

**Diagnostics** are warnings or errors reported by aspects. They provide feedback about code issues during compilation and in the IDE.

## Extension packages

Metalama provides extension packages that add capabilities through an extensibility mechanism. These packages integrate with the core framework but are distributed separately.

> [!NOTE]
> The extensibility mechanism used by these packages is public but not part of the supported SDK and is subject to change.

```mermaid
flowchart TB
    subgraph Extensions
        Validators
        CodeFixes[Code Fixes]
    end
    SourceCode[Source Code]
    Aspects -- register --> Validators
    Fabrics -- register --> Validators
    Validators -- report & suppress --> Diagnostics
    Diagnostics -- contain --> CodeFixes
    CodeFixes -- apply --> Aspects
    SourceCode -- contains --> Fabrics
    SourceCode -- contains --> Aspects
```

**Validators** (from <xref:Metalama.Extensions.Validation>) perform code analysis. They can be registered by aspects or fabrics, and report or suppress diagnostics based on your code's compliance with defined rules.

**Code fixes** (from <xref:Metalama.Extensions.CodeFixes>) are suggested solutions attached to diagnostics. They appear in the IDE lightbulb menu and can apply aspects to fix issues automatically.

> [!div class="see-also"]
> <xref:aspects>
> <xref:fabrics>
> <xref:aspect-design>
> <xref:advising-code>
> <xref:diagnostics>
> <xref:validation>
