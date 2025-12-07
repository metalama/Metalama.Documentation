---
uid: fabrics-execution-order
level: 400
summary: This document explains the execution order of fabrics in a project, including project fabrics, transitive project fabrics, namespace fabrics, type fabrics, and aspects, with criteria for each category.
keywords: "execution order, dependency graph, project configuration"
created-date: 2023-02-17
modified-date: 2025-11-30
---

# Execution order of fabrics

Fabrics are executed in the following order:

1. **Project fabrics**. The execution order of project fabrics is determined by the following criteria:

    1. The distance of the source file from the root directory. Fabrics closer to the root directory are processed first.
    2. The fabric namespace.
    3. The fabric type name.

2. **Transitive project fabrics**. The execution order of transitive project fabrics is determined by the following criteria:

     1. The depth in the dependency graph. Dependencies with lower depth (i.e., closer to the main project) are processed first.
     2. The assembly name, in alphabetical order.

    Note that transitive dependencies are intentionally executed after compilation dependencies, allowing the latter to configure the former before they run.

3. At this stage, Metalama freezes the project configuration by invoking <xref:Metalama.Framework.Project.ProjectExtension.MakeReadOnly> on all configuration objects. Consequently, the execution order of the following fabrics shouldn't have any impact.

4. **Namespace fabrics**.

5. **Type fabrics**. Note that type fabrics can provide advice, which is executed before any aspect.

6. **Aspects**. For information about the execution order of explicitly ordered and unordered aspects, see <xref:ordering-aspects>.

> [!div class="see-also"]
> <xref:fabrics>
> <xref:ordering-aspects>
> <xref:implementation>
> <xref:pipeline>
> <xref:Metalama.Framework.Fabrics.ProjectFabric>
> <xref:Metalama.Framework.Fabrics.NamespaceFabric>
> <xref:Metalama.Framework.Fabrics.TypeFabric>
