---
uid: architecture
level: 300
summary: "The document describes the architecture of Metalama, showing how aspects, validators, code fixes, advice, source code, fabrics, and diagnostics interact."
keywords: "Metalama architecture, aspects, validators, code fixes, advice, source code, fabrics, diagnostics, code transformations"
created-date: 2023-07-11
modified-date: 2025-11-30
---
# Metalama architecture

```mermaid
flowchart  TB
    Aspects -- report & suppress --> Diagnostics
    Aspects -- register --> Validators
    Aspects -- suggest --> CodeFixes
    Aspects -- provide --> Advice
    Advice -- provide --> Transformation[Code Transformations]
    SourceCode[Source Code] -- annotated with<br>custom attributes or explicit --> Aspects
    SourceCode -- contains --> Fabrics
    Fabrics -- provide --> Aspects
    Fabrics -- provide --> Validators
    Validators -- provide & suppress--> Diagnostics
    Diagnostics -- contain --> CodeFixes
    CodeFixes[Code Fixes] -- apply --> Aspects
```

> [!div class="see-also"]
> <xref:aspects>
> <xref:fabrics>
> <xref:aspect-design>
> <xref:advising-code>
> <xref:diagnostics>

