---
uid: Metalama.Extensions.CodeFixes
summary: *content
created-date: 2023-01-26
modified-date: 2023-07-28
---

This namespace enables you to suggest code fixes and code refactorings, which are changes to source code that appear in the lightbulb or screwdriver menu of your IDE.

You can instantiate code fixes using the static methods of the <xref:Metalama.Extensions.CodeFixes.CodeFixFactory> class.

To add code fixes to a diagnostic, utilize the <xref:Metalama.Extensions.CodeFixes.CodeFixExtensions.WithCodeFixes*?text=IDiagnostic.WithCodeFixes> extension method.

To suggest a code refactoring without reporting a diagnostic, employ the <xref:Metalama.Extensions.CodeFixes.CodeFixExtensions.Suggest*>text=ScopedDiagnosticSink.Suggest> method.

## Class diagram

```mermaid
classDiagram

    class ScopedDiagnosticSink {
        Report()
        Suppress()
        Suggest()
    }

    class IDiagnostic {
        CodeFixes
        WithCodeFixes(CodeFixes[])
    }

  class CodeFix {
        Id
        Title
    }
    class CodeFixFactory {
        ApplyAspect()$
        AddAttribute()$
        RemoteAttribute()$
        CreateCustomCodeFix(Func~ICodeFixBuilder,Task~)$
    }
    class ICodeFixBuilder {
       ApplyAspectAsync()
       AddAttributeAsync()
       RemoteAttributeAsync()
    }

    ICodeFixBuilder <-- CodeFixFactory : custom code fixes\nimplemented with
    CodeFix <-- CodeFixFactory : creates
    IDiagnostic <-- CodeFix: add to
    ScopedDiagnosticSink <-- CodeFix: suggest to
    ScopedDiagnosticSink <-- IDiagnostic: reports to
```

## Namespace members

