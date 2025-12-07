---
uid: building-ide-interactions
level: 200
summary: "Guidance on adding custom actions to IDE menus using Metalama, highlighting benefits like fewer keystrokes, less documentation lookup, improved team alignment, and enhanced developer productivity."
keywords: "developer productivity, code fixes, refactorings, improve productivity"
created-date: 2023-02-17
modified-date: 2025-11-30
---

# Building IDE interactions

> [!NOTE]
> This feature requires a Metalama Professional license.

You're likely familiar with the code fixes and refactorings your IDE offers via the screwdriver or lightbulb icons in the editor. Most of these code actions are programmed by the manufacturer of your IDE.

This chapter shows you how to add your own actions to the screwdriver or lightbulb menus and integrate them with other Metalama features.

## Benefits

Building custom IDE interactions for your architecture and team offers the following benefits:

* **Fewer keystrokes**: Developers save typing time and effort.
* **Less documentation lookup**: A code fix provides implicit implementation guidance, eliminating the need for team members to refer to the documentation to fix the custom warning or error reported by your aspect.
* **Improved team alignment**: Providing live templates for frequently occurring implementation scenarios encourages the team to follow common development patterns, leading to a more consistent codebase.
* **Higher developer productivity**: Providing developers with custom code fixes relevant to their current code enhances their productivity.

## In this chapter

This chapter includes the following articles:

| Article | Description |
|---------|-------------|
| <xref:live-template> | Demonstrates how to expose an aspect as a code refactoring that can be applied directly to _source_ code, instead of the executable code in the background. |
| <xref:code-fixes> | Explains how aspects and validators can suggest code fixes and refactorings. |

> [!div class="see-also"]
> <xref:aspects>
> <xref:eligibility>
> <xref:diagnostics>
> <xref:Metalama.Framework.Aspects.EditorExperienceAttribute>
