---
uid: aspect-configuration
summary: "Making aspects configurable allows centralized aspect options and improved run-time performance through the configuration API and MSBuild properties."
level: 200
keywords: "aspect configuration, centralized aspect options, configuration API, MSBuild properties, compile-time options, custom attribute"
created-date: 2024-07-22
modified-date: 2025-11-30
---

# Making aspects configurable

Complex and widely-used aspects often require a centralized, project-wide method for configuring their compile-time behavior. Implementing a configuration for aspects provides the following benefits:

* **Centralized aspect options**: A configuration API allows the entire project, namespaces, or class families to be configured from a single location. Without a configuration API, users must supply the configuration each time a custom attribute is used.

* **Run-time performance**: When you consider compile-time options, your aspect can generate optimal run-time code for higher run-time performance.

Metalama provides two complementary mechanisms for configuration: the configuration API and MSBuild properties.

> [!NOTE]
> Compile-time configuration has significantly changed with Metalama 2023.4. If you're looking for the previous configuration API, see <xref:exposing-configuration-before-2023-4>.

## In this chapter

| Article | Description |
|---------|-------------|
| <xref:exposing-options> | This article explains how to build a programmatic configuration API that can be called from fabrics. |
| <xref:reading-msbuild-properties> | This article describes how your aspect can consume an MSBuild property. |
| <xref:configuration-custom-merge> | This article explains in detail the options merging process, and how to customize it. |

> [!div class="see-also"]
> <xref:aspects>
> <xref:fabrics>
> <xref:Metalama.Framework.Options>
