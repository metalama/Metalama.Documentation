---
uid: live-template
level: 200
summary: "The document explains how to create a live template using the Metalama Aspect Framework, which appears in the code editor menu alongside other code suggestions or refactoring actions."
keywords: "live template, Metalama Aspect Framework, code editor menu, code suggestions, refactoring actions, EditorExperience, aspect eligibility"
created-date: 2023-02-17
modified-date: 2024-08-04
---

# Exposing an aspect as a live template

> [!NOTE]
> This feature requires a Metalama Professional license. The `Metalama.Extensions.CodeFixes` package must be added to the project defining the aspects.

A _live template_ is a custom Code Action that appears in the code editor menu alongside other code suggestions or refactoring actions offered by the Integrated Development Environment (IDE). For more information on using live templates, please refer to <xref:applying-live-templates>.

Live templates are created using the Metalama Aspect Framework. Unlike traditional aspects that are executed at compile-time by the compiler, live templates are interactively applied by the user within the editor, thereby modifying the source code.

> [!NOTE]
> A key characteristic of an aspect is that it is applied at compile time and does not alter the source code. Consequently, a live template, despite being built with the aspect framework, cannot be referred to as an aspect as it deviates from this fundamental principle. To prevent any confusion, we recommend avoiding the use of the term 'aspects' when discussing live templates.

## To write a live template

1. Begin by writing an aspect class as you normally would, but keep in mind the following differences:
   - The aspect class does not need to inherit from `System.Attribute`.
   - Strive to generate idiomatic C# code.
   - Any diagnostics reported by the aspect will be disregarded.
   - Aspect ordering and requirements will not be considered.
2. Ensure that the aspect class is equipped with a default constructor.
3. Annotate the class with `[EditorExperience(SuggestAsLiveTemplate = true)]`.
4. Properly define the aspect eligibility to ensure that the code refactoring is suggested only for relevant declarations. For more details, refer to <xref:eligibility>.

> [!div class="see-also"]
> <xref:video-code-fixes>
> <xref:code-fixes>


