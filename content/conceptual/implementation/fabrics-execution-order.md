---
uid: fabrics-execution-order
level: 400
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

    Please note that transitive dependencies are intentionally executed after compilation dependencies, allowing the latter to configure the former before they run.

3. At this stage, the project configuration is frozen by invoking <xref:Metalama.Framework.Project.ProjectExtension.MakeReadOnly> on all configuration objects. Consequently, the execution order of the following fabrics should not have any impact.

4. **Namespace fabrics**.

5. **Type fabrics**. It's important to note that type fabrics can provide advice, which is executed before any aspect.

6. **Aspects**. For information regarding the execution order of explicitly ordered and unordered aspects, please refer to <xref:ordering-aspects>.


