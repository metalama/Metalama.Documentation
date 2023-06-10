---
uid: aspects
level: 200
---

# Creating aspects

This chapter explains how to build your own aspects. If you only want to use aspects written by others, you can skip this chapter. However, we suggest you revisit this chapter later to understand better how Metalama works.

## Benefits

* **Boilerplate elimination**. When you use the code transformation abilities of aspects to create boilerplate at compile time, you get the following benefits:

  * **Less code to write**. You no longer have to write boilerplate code because your aspect generates it.
  * **Clean and readable code**. Your source code is cleaner, and you have fewer lines of code, therefore easier to understand.
  * **Fewer bugs**. Since there is less code and clearer code, there should be fewer bugs.
  * **Deduplication**. Cross-cutting patterns are defined in one place, so if you need to change or fix them, you do not need to fix all occurrences in your code base.

* **Code validation**. You can use aspects to create custom attributes that validate code. For details and benefits, see <xref:aspect-validating>.
* **Code fixes**. You can use aspects to offer code fixes that appear in the refactoring or lightbulb menu. For details, see <xref:building-ide-interactions>.

## In this chapter

This chapter is composed of the following articles:

| Article                       | Description                                                                                                                                            |
| ----------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------
| <xref:templates>                    | This section explains how to create dynamic code templates.                                                                                            |
| <xref:advising-code>                | This section explains how to create aspects that perform advanced code modifications using the complete API.                                           |
| <xref:diagnostics>                  | This section explains how to report errors, warnings, and information messages and suppress warnings, see <xref:diagnostics>.                                                             |
| <xref:dependency-injection>         | This section describes how an aspect can use a dependency and pull it from the container.                                                             |
| <xref:building-ide-interactions> | This article explains how to create live templates, code fixes, and refactorings.                                                    |
| <xref:reusing-code>                 | This section describes how to reuse code among aspects and templates. |
| <xref:child-aspects>                | This section explains how an aspect can add other aspects and how child aspects can know about their parents.          |
| <xref:aspect-inheritance>            | This section explains how to automatically apply an aspect to all declarations derived from its direct targets.  |
| <xref:ordering-aspects>       | This article describes how to order aspect classes so that the order of execution is correct when several aspects are applied to the same declaration. |
| <xref:exposing-configuration>       | This article explains how an aspect can expose and consume configuration properties or a configuration API. |
| <xref:testing>                      | This section explains how to test aspects.                                                                                                             |
| <xref:debugging-aspects>            | This article explains how to debug aspects.                                                                                                            |
