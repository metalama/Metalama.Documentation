---
uid: aspect-api
summary: "This API documentation describes the Metalama Framework, detailing the namespaces and their functionalities for creating and managing aspects or fabrics."
keywords: "Metalama Framework, API documentation, creating aspects, managing aspects, source code representation, code fixes, diagnostics, eligibility, project model, runtime classes"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Aspect API documentation

These namespaces are used when creating aspects or fabrics.

| Namespace                             | Description                                                                                                                                                     |
|---------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Framework.Aspects>     | Main namespace for creating aspects, including base classes like <xref:Metalama.Framework.Aspects.TypeAspect>, <xref:Metalama.Framework.Aspects.MethodAspect>, and <xref:Metalama.Framework.Aspects.FieldOrPropertyAspect>. Also contains <xref:Metalama.Framework.Aspects.AdviserExtensions>, the first-level API for programmatically adding advice.                                                                                                           |
| <xref:Metalama.Framework.Advising>    | Contains second-level APIs used when advising code behind extension methods of <xref:Metalama.Framework.Aspects.AdviserExtensions>.               |
| <xref:Metalama.Framework.Diagnostics> | Enables aspects to report or suppress errors, warnings, or informational messages using <xref:Metalama.Framework.Diagnostics.IDiagnosticSink>.                                                                                   |
| <xref:Metalama.Framework.Eligibility> | Allows aspects to declare eligibility rules that specify which declarations they can be applied to.                                                                            |
| <xref:Metalama.Framework.Fabrics>    | Provides the ability to add aspects or validators to entire projects and namespaces using <xref:Metalama.Framework.Fabrics.ProjectFabric> and <xref:Metalama.Framework.Fabrics.NamespaceFabric>, and to configure aspects.                                             |
| <xref:Metalama.Framework.Metrics>        | Read predefined code metrics and implement custom metrics. Metrics are useful in validators and introspection queries.                               |
| <xref:Metalama.Framework.Project>        | Exposes the object model of the project being processed through <xref:Metalama.Framework.Project.IProject>, as well as the service provider.                                                               |
| <xref:Metalama.Framework.RunTime>     | Contains classes used at runtime. All other namespaces are used only at compilation time.                                                              |

> [!div class="see-also"]
> <xref:aspects>
> <xref:fabrics>
