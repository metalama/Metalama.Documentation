---
uid: validation
level: 200
summary: "The document discusses verifying source code against architecture, design patterns, and team conventions, emphasizing the benefits of immediate feedback, smoother code reviews, team alignment, reduced complexity, and architecture erosion prevention."
keywords: "code verification, architecture validation, design patterns, team conventions, immediate feedback, smoother code reviews, team alignment, reduced complexity, architecture erosion prevention, Metalama"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Verifying architecture

> [!NOTE]
> This feature requires a Metalama Professional license.

This chapter outlines how to verify your source code against the architecture, design patterns, and other team conventions.

There are two methods for adding verification rules to your code. You can do this declaratively by applying custom architecture attributes to your code, or programmatically using a compile-time fluent API. Metalama provides a set of pre-made custom attributes and compile-time methods. Additionally, you can easily create your own attributes or methods for rules that are specific to your project.

## Benefits

Verifying code against architecture is particularly important for projects developed by a large team or maintained over a long period.

* **Executable rules instead of paper guidelines**: Architectural guidelines can now be enforced in real-time within the code editor, rather than merely being written down and stored away.
* **Immediate feedback**: Developers don't have to wait for the CI build to finish; feedback is provided within seconds.
* **Smoother code reviews**: Rule violations are automatically detected, allowing code reviews to focus on flows and concepts.
* **Better team alignment**: Automated code validation promotes the team's adherence to consistent patterns and practices.
* **Lower complexity**: The resulting codebase is simpler when everyone on the team adheres to consistent patterns and practices.
* **Reduced architecture erosion**: The gap between the initial architecture and its implementation in the source code remains smaller.

## In this chapter

This chapter includes the following articles:

|Article  |Description  |
|---------|---------|
|<xref:validating-usage>     |  Validate the _usage_ of namespaces, types, or members, restricting who can access them.       |
|<xref:naming-conventions> | Enforce naming conventions in your code. |
|<xref:experimental> | Mark an API as experimental, triggering a warning when the API is used. |
|<xref:internal-only-implement> | Restrict who can implement an interface. |
|<xref:validation-extending>     |  Create custom attributes or fabric extension methods to validate your own architectural rules.   |

> [!div class="see-also"]
> <xref:Metalama.Extensions.Architecture>
> <xref:fabrics>



