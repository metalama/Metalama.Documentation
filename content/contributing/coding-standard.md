---
uid: coding-standard
level: 200
summary: "This document outlines the coding standards to follow when contributing to the Metalama project."
keywords: "coding standard, coding conventions"
created-date: 2025-05-14
modified-date: 2026-08-04
---

# Coding standard

## C# code

### Baseline

This project follows the [common C# coding conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) and [C# identifier naming rules and conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names).

### Enforcement

The coding standard is enforced using a combination of two tools:

- The standard `.editorconfig`, which is configured to generate warnings during builds. All warnings must be resolved before submitting a pull request (PR).
- JetBrains' code style settings, which provide more comprehensive rules than `.editorconfig`. These settings aren't enforced for every PR.

### Automatic formatting

For day-to-day development, use the code formatting feature of your IDE. Both Visual Studio's and Rider's code cleanup tools are supported.

### Quality criteria for releases

Certain quality criteria aren't covered by build warnings and are enforced manually before each release.

Prior to every release, we perform a complete code cleanup using the following script:

```powershell
Build.ps1 codestyle format
```

> [!WARNING]
> Don't reformat files that you haven't directly modified in your PR.

Additionally, we run the full JetBrains code quality inspection suite before each release. Execute it from a Visual Studio Code terminal using the following script:

```powershell
Build.ps1 codestyle inspect
```

## Tests

All new features must be thoroughly tested. The code must work with _any_ valid C# code. Our policy is that Metalama should support _all_ valid C# code, and users should never be required to refactor their code to accommodate Metalama.

> [!div class="see-also"]
> <xref:contribute-code>
> <xref:build-from-source>
