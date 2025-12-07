---
uid: advising-code
level: 300
summary: "The document provides a list of articles discussing advanced techniques in transforming code, including overriding methods, fields, events, implementing interfaces, and using aspects."
keywords: "overriding methods, overriding fields, implementing interfaces, advanced techniques, aspects, transforming code, overriding events, introducing members, validating values, initialization logic"
created-date: 2023-02-20
modified-date: 2025-11-30
---

# Transforming code

In <xref:simple-aspects>, you learned how to build simple aspects composed of a single transformation (or advice). This section introduces more advanced scenarios and explains how to create aspects composed of multiple pieces of advice. Before diving deeper into these topics, familiarize yourself with a few additional concepts.

| Article | Description |
|----|----|
| <xref:advising-concepts> | Explains essential concepts. We recommend reading this article first. |
| <xref:overriding-methods> | Advanced techniques for overriding methods. Continuation of <xref:simple-override-method>. |
| <xref:overriding-fields-or-properties> | Advanced techniques for overriding fields and properties. Continuation of <xref:simple-override-property>. |
| <xref:overriding-events> | Techniques for overriding events. |
| <xref:contracts> | Advanced techniques for validating field, property, or parameter values using contracts. Continuation of <xref:simple-contracts>. |
| <xref:introducing-members> | Explains how to introduce new members to an existing type using aspects. |
| <xref:implementing-interfaces> | Details how to make an existing type implement a new interface using aspects. |
| <xref:introducing-types> | Explains how to introduce new types (both top-level and nested types). |
| <xref:initializers> | Describes how to add initialization logic to constructors. |
| <xref:introducing-constructor-parameters> | Explains how to add new parameters to constructors and retrieve their value from constructors of derived classes. |
| <xref:overriding-constructors> | Explains how to override constructors. |
| <xref:adding-attributes> | Explains how to add or remove custom attributes. |
| <xref:sharing-state-with-advice> | Discusses how the `BuildAspect` method can pass parameters or state to templates. |

> [!div class="see-also"]
> <xref:simple-aspects>
> <xref:templates>
> <xref:Metalama.Framework.Aspects.AdviserExtensions>
