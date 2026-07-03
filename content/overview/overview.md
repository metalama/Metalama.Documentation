---
uid: overview
level: 100
summary: "Metalama is an open-source patterns and architecture toolkit for C#: it implements your patterns at compile time and enforces your architecture rules as you type."
keywords: "Metalama, patterns and architecture toolkit, design patterns, Aspect-Oriented Programming, Code Generation, Code Validation, code readability, repetitive code, team rules, .NET, encapsulate repetitive patterns, generate repetitive code, verify code compliance"
created-date: 2023-02-16
modified-date: 2026-07-03
---

# Overview

This book will guide you through evaluating Metalama. It won't teach you how to deploy and use it, but rather why and whether it's suitable for you.

Metalama is an open-source patterns and architecture toolkit for C#. You define your team's patterns and rules once: the compiler generates the repetitive code at build time and enforces your architecture rules in real time, as you type.

## Features

Metalama provides the following main features:

| Feature | Description |
|---------|-------------|
| **Aspect-Oriented Programming** | Encapsulate repetitive code patterns (such as logging, caching, `INotifyPropertyChanged`, multi-threading) into executable artifacts called _aspects_. Aspects add behaviors to your code at compile time, keeping your source code clean and concise—easier to read and maintain.
| **Code Generation**             | Generate repetitive code in the editor or at compile time instead of writing it manually. Create your own code actions or refactorings that appear in the lightbulb or screwdriver menu.
| **Code Validation**             | Verify that manually written code complies with team rules and conventions. Report diagnostics (warnings or errors) or suppress source code diagnostics.

```mermaid
graph TD

Aspects[Aspect &<br>Fabrics] -- report and suppress --> Diagnostics
Aspects -- suggest --> CodeFixes[Code Fixes &<br>Refactorings]
CodeFixes -- transform<br>at design time --> SourceCode[Source Code]
Aspects -- transform<br>at compile time --> TransformedCode[Compiled Code]

```

> [!div class="see-also"]
>
> <xref:index>
> <xref:main-getting-started>
> <xref:conceptual>
> <xref:aspects>
