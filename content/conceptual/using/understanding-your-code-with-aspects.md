---
uid: understanding-your-code-with-aspects
level: 100
summary: "This document explains how to understand aspect-oriented code using Metalama's tools like CodeLens, Diff Preview, and Debug Transformed Code. It also discusses explicit and implicit aspect applications."
keywords: "aspect-oriented code, Metalama tools, CodeLens, Metalama Diff, Debug Transformed Code, understand code functionality, aspect explorer"
created-date: 2023-02-16
modified-date: 2025-12-07
---

# Understanding your aspect-oriented code

> [!NOTE]
> These features require a Metalama Community or Metalama Professional license.

> [!NOTE]
> These features are only available in Visual Studio when Visual Studio Tools for Metalama are installed. See <xref:ide-configuration> for IDE-specific information.

After integrating aspects into your code, you might be curious about its functionality and execution process. Metalama provides several tools to help you understand what happens with your code when you hit the Run button.

These tools include:

- CodeLens
- Metalama Diff
- Aspect Explorer

## CodeLens

CodeLens displays the number of aspects applied to your code directly in the editor. Click the summary for more details:

![](./images/log_aspect_applied_on_flakymethod.png)

CodeLens reveals the following details:

|Detail | Purpose |
|-------|---------|
|Aspect Class  | The name of the aspect applied to this target. |
|Aspect Target | The fully qualified name of the target. |
|Aspect Origin | How the aspect is applied. |
|Transformation| A default message indicating that the aspect alters the behavior of the target method. |

This feature is particularly useful when many aspects are added to your code or when aspects are applied implicitly. Even if there's no aspect custom attribute on your code, an aspect may still be applied through aspect inheritance (when the aspect is applied to an ancestor class or interface) or fabrics (which add aspects in bulk without custom attributes).

## Metalama Diff

After discovering that aspects affect your code, you might wonder how. The simplest approach is to compare your source code with the generated code side by side in a diff. This is what Metalama Diff provides.

To preview the change, click the `Preview Transformed Code` link in Code Lens. Alternatively, right-click the document and choose _Metalama Diff_ from the context menu.

![Metalama_Diff_Menu_Option](images/showing_metalama_diff_option.png)

The result displays as follows:

![Metalama_Diff_Side_by_Side](images/lama_diff_side_by_side.png)

> [!NOTE]
> Access this preview dialog by pressing `Ctrl + K` followed by `0`.

The screenshot above displays the source of `FlakyMethod` and the code modified by the `[Log]` aspect. However, the command shows the entire file in its original and modified versions side by side.

To view changes for a specific section of the code, select that section from the dropdown.

![Diff_change_selector](images/metalama_diff_change_view_selector.png)

## Aspect Explorer

With CodeLens, you start from a specific piece of code and wonder which aspects influence it.

Aspect Explorer answers the opposite question. It shows all aspects available in the current solution, and when you select one, it displays the declarations it affects.

To open the Aspect Explorer tool window, use the top-level menu and select _Extension_ > _Metalama + PostSharp_ > _Aspect Explorer_.

![Aspect Explorer](images/aspect-explorer.png)

> [!div class="see-also"]
>
> **See also**
>
> <xref:using-metalama>
> <xref:quickstart-adding-aspects>
> <xref:debugging-aspect-oriented-code>
> <xref:install-vsx>
