---
uid: live-template
level: 200
summary: "Explains how to create a live template using the Metalama Aspect Framework, which appears in the code editor menu alongside other code suggestions or refactoring actions."
keywords: "live template, Metalama Aspect Framework, code editor menu, code suggestions, refactoring actions, EditorExperience, aspect eligibility"
created-date: 2023-02-17
modified-date: 2025-11-30
---

# Exposing an aspect as a live template

A _live template_ is a custom code action that appears in the code editor menu alongside other code suggestions or refactoring actions offered by your IDE. For more information on using live templates, see <xref:applying-live-templates>.

Live templates are created using the Metalama Aspect Framework. Unlike traditional aspects that are executed at compile time by the compiler, live templates are interactively applied by you within the editor, modifying the source code.

> [!NOTE]
> A key characteristic of an aspect is that it's applied at compile time and doesn't alter the source code. Consequently, a live template, despite being built with the aspect framework, can't be referred to as an aspect because it deviates from this fundamental principle. To prevent confusion, avoid using the term "aspects" when discussing live templates.

## Writing a live template

1. Write an aspect class as you normally would, keeping in mind the following differences:
   - The aspect class doesn't need to inherit from `System.Attribute`.
   - Generate idiomatic C# code.
   - Any diagnostics reported by the aspect are ignored.
   - Aspect ordering and requirements aren't considered.
2. Add the `Metalama.Extensions.CodeFixes` package to your project.
3. Ensure the aspect class has a default constructor.
4. Annotate the class with `[EditorExperience(SuggestAsLiveTemplate = true)]`.
5. Define the aspect eligibility to ensure the code refactoring is suggested only for relevant declarations. For more details, see <xref:eligibility>.

> [!div class="see-also"]
> <xref:code-fixes>
> <xref:building-ide-interactions>
> <xref:eligibility>
> <xref:applying-live-templates>
> <xref:Metalama.Framework.Aspects.EditorExperienceAttribute>
> <xref:Metalama.Framework.Aspects.IAspect`1>
