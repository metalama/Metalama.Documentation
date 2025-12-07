---
uid: aspects
level: 200
summary: "This document is a guide on creating aspects in Metalama, detailing the benefits of aspects like boilerplate elimination, code validation and fixes, and outlining various aspects-related topics."
keywords: "boilerplate elimination, code validation, code fixes, Metalama, creating aspects, clean code, fewer bugs, custom attributes, dynamic code templates, advanced code modifications"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Creating aspects

This chapter provides a comprehensive guide on how to build your own aspects. If you're only interested in using pre-existing aspects, you can initially skip this chapter. However, we recommend revisiting this chapter later to gain a deeper understanding of how Metalama operates.

## Benefits

* **Boilerplate elimination**: Using the code transformation capabilities of aspects allows you to generate boilerplate code at compile time, offering the following benefits:

  * **Less code to write**: The aspect generates boilerplate code, eliminating the need for you to write it.
  * **Clean and readable code**: Your source code becomes cleaner and more concise, making it easier to understand.
  * **Fewer bugs**: The reduction in code volume and increase in clarity is likely to result in fewer bugs.
  * **Deduplication**: Cross-cutting patterns are defined in one place, meaning that changes or fixes need only be applied once, rather than across multiple occurrences in your codebase.

* **Code validation**: Aspects can be used to create custom attributes that validate code. For further details and associated benefits, refer to <xref:aspect-validating>.
* **Code fixes**: Aspects can also be used to provide code fixes that appear in the refactoring or lightbulb menu. For more information, see <xref:building-ide-interactions>.

## In this chapter

This chapter includes the following articles:

| Article                       | Description                                                                                                                                            |
| ----------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------ |
| <xref:templates>             | This section provides guidance on how to create dynamic code templates.                                                                                |
| <xref:advising-code>         | This section explains how to create aspects that perform advanced code modifications using the complete API.                                           |
| <xref:diagnostics>           | This section outlines how to report errors, warnings, and information messages and suppress warnings. See <xref:diagnostics> for more details.          |
| <xref:dependency-injection>  | This section describes how an aspect can utilize a dependency and retrieve it from the container.                                                      |
| <xref:building-ide-interactions> | This article discusses how to create live templates, code fixes, and refactorings.                                                                    |
| <xref:child-aspects>         | This section explains how an aspect can add other aspects and how child aspects can become aware of their parents.                                     |
| <xref:aspect-inheritance>    | This section provides insight into how to automatically apply an aspect to all declarations derived from its direct targets.                           |
| <xref:multiple-instances>    | This section explains how to handle multiple aspect instances on the same declaration, including ordering different types and handling multiple instances of the same type. |
| <xref:configuration> | This article explains how an aspect can expose and consume configuration properties or a configuration API.                                           |
| <xref:testing>               | This section provides guidance on how to test aspects.                                                                                                 |
| <xref:debugging-aspects>     | This article explains how to debug aspects.                                                                                                            |

> [!div class="see-also"]
> <xref:simple-aspects>
> <xref:using-metalama>
> <xref:Metalama.Framework.Aspects>
