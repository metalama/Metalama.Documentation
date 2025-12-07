---
uid: advising-code
level: 300
summary: "The document provides a list of articles discussing advanced techniques in transforming code, including overriding methods, fields, events, implementing interfaces, and using aspects."
keywords: "overriding methods, overriding fields, implementing interfaces, advanced techniques, aspects, transforming code, overriding events, introducing members, validating values, initialization logic"
created-date: 2023-02-20
modified-date: 2025-11-30
---

# Transforming code

In <xref:simple-aspects>, you learned how to build simple aspects composed of a single transformation (or advice). This section introduces more advanced scenarios and explains how to create aspects composed of multiple pieces of advice. However, before we go deeper into these topics, we need to familiarize you with a few additional concepts.

| Article | Description |
|----|----|
| <xref:advising-concepts> | This article elucidates essential concepts. We recommend reading this article first before proceeding to the article that covers the advice you need. |
| <xref:overriding-methods> | This article introduces advanced techniques for overriding methods. It is the continuation of <xref:simple-override-method>. |
| <xref:overriding-fields-or-properties> | This article offers advanced techniques for overriding fields and properties. It is the continuation of <xref:simple-override-property>. |
| <xref:overriding-events> | This article demonstrates techniques for overriding events. |
| <xref:contracts> | This article introduces advanced techniques for validating the values of fields, properties, or parameters using contracts. It is the continuation of <xref:simple-contracts>. |
| <xref:introducing-members> | This article explains how to introduce new members to an existing type using aspects. |
| <xref:implementing-interfaces> | This article details how to make an existing type implement a new interface using aspects. |
| <xref:introducing-types> | This article explains how to introduce new types (both top-level types or nested types). |
| <xref:initializers> | This article describes how to add initialization logic to constructors. |
| <xref:introducing-constructor-parameters> | This article explains how to add new parameters to constructors and retrieve their value from constructors of derived classes. |
| <xref:overriding-constructors> | This article explains how to override constructors. |
| <xref:adding-attributes> | This article explains how to add or remove custom attributes. |
| <xref:sharing-state-with-advice> | This article discusses how the `BuildAspect` method can pass parameters or state to the templates. |

> [!div class="see-also"]
> <xref:simple-aspects>
> <xref:templates>
> <xref:Metalama.Framework.Aspects.AdviserExtensions>
