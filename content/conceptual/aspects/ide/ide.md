---
uid: building-ide-interactions
level: 200
summary: "The document provides guidance on how to add custom actions to IDE menus using Metalama, highlighting the benefits such as fewer keystrokes, less documentation lookups, improved team alignment, and enhanced developer productivity."
keywords: "developer productivity, code fixes, refactorings, improve productivity"
created-date: 2023-02-17
modified-date: 2024-08-04
---

# Building IDE interactions

> [!NOTE]
> This features requires a Metalama Professional license.

Likely, you are familiar with the code fixes and refactorings that your IDE offers via the screwdriver or lightbulb icons in the editor. Most of these code actions have been programmed by the manufacturer of your IDE.

In this chapter, we will guide you on how to add your own actions to the screwdriver or lightbulb menus and integrate them with the other features of Metalama.

## Benefits

Building custom IDE interactions for your architecture and team offers the following benefits:

* **Fewer keystrokes**: Developers can save typing time and effort.
* **Fewer documentation lookups**: A code fix provides implicit implementation guidance, eliminating the need for team members to refer to the documentation to fix the custom warning or error reported by your aspect.
* **Improved team alignment**: By providing live templates for frequently occurring implementation scenarios, you encourage the team to follow common development patterns. This practice leads to a more consistent codebase.
* **Higher developer productivity**: By providing developers with custom code fixes relevant to their current code, you can enhance their productivity.

## In this chapter

This chapter includes the following articles:

| Article | Description |
|---------|-------------|
| <xref:live-template> | This article demonstrates how to expose an aspect as a code refactoring that can be applied directly to _source_ code, instead of the executable code in the background. |
| <xref:code-fixes> | This article explains how aspects and validators can suggest code fixes and refactorings. |





