---
uid: validation
level: 200
summary: "Verify your source code against architecture, design patterns, and team conventions using Metalama's validation features."
keywords: "code verification, architecture validation, design patterns, team conventions, immediate feedback, smoother code reviews, team alignment, reduced complexity, architecture erosion prevention, Metalama"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Verifying architecture

> [!NOTE]
> This feature requires a Metalama Professional license.

This chapter explains how to verify your source code against architecture, design patterns, and other team conventions.

You can add verification rules to your code in two ways: declaratively by applying custom architecture attributes, or programmatically using a compile-time fluent API. Metalama provides pre-made custom attributes and compile-time methods. You can also create your own attributes or methods for project-specific rules.

## Benefits

Verifying code against architecture is particularly important for projects developed by a large team or maintained over a long period.

* **Executable rules instead of paper guidelines**: Architectural guidelines are enforced in real time within the code editor, not just written down and stored away.
* **Immediate feedback**: Developers receive feedback within seconds without waiting for the CI build to finish.
* **Smoother code reviews**: Rule violations are automatically detected, allowing code reviews to focus on flows and concepts.
* **Better team alignment**: Automated code validation promotes the team's adherence to consistent patterns and practices.
* **Lower complexity**: The resulting codebase is simpler when everyone on the team adheres to consistent patterns and practices.
* **Reduced architecture erosion**: The gap between the initial architecture and its implementation in the source code remains smaller.

## In this chapter

This chapter includes the following articles:

|Article  |Description  |
|---------|---------|
|<xref:validating-usage>     |  Validate the _usage_ of namespaces, types, or members by restricting who can access them.       |
|<xref:naming-conventions> | Enforce naming conventions in your code. |
|<xref:experimental> | Mark an API as experimental to trigger a warning when it's used. |
|<xref:internal-only-implement> | Restrict who can implement an interface. |
|<xref:validation-extending>     |  Create custom attributes or fabric extension methods to validate your own architectural rules.   |

> [!div class="see-also"]
> <xref:Metalama.Extensions.Architecture>
> <xref:fabrics>
