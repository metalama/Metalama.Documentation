---
uid: migration
summary: "The document provides guidance on migrating projects from PostSharp to its successor, Metalama, including rewriting aspects, maintaining business code, and various articles to assist the migration process."
keywords: "migration, PostSharp, Metalama, rewrite aspects"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Migrating from PostSharp

You may want to migrate some or all of your projects from PostSharp to Metalama. If you decide to proceed, this chapter will help you.

When we conceptualized Metalama as the successor to PostSharp, we chose to break backward compatibility. The PostSharp Framework was designed during the years of C# 2.0 and .NET Framework 2.0 (2004-2010), and we've maintained backward compatibility since then. We couldn't leverage the new .NET stack (C# 11, .NET 6, and Roslyn) while adhering strictly to our 2010 API. We opted for a comprehensive redesign of the concepts and APIs.

However, many customers have codebases ranging from tens of thousands to millions of lines of code that use PostSharp. We ensured these customers wouldn't need to port these large codebases.

We made the following decisions:

* You'll need to completely rewrite your _aspects_.
* The _business code_ that _uses_ the aspects typically doesn't require any changes, except for find-and-replace-in-files operations and namespace import replacements.

## In this chapter

Article | Description
-|-
<xref:migrating-aspects> | Provides step-by-step guidance for your migration project and refers to other articles in this chapter.
<xref:benefits-over-postsharp> | Describes the advantages of Metalama over PostSharp.
<xref:when-migrate> | Provides points to consider before migrating your aspects to Metalama. Read this article before making any decisions.
<xref:migration-feature-status> | Describes the status of PostSharp features in Metalama.
<xref:differences-from-postsharp> | Discusses the significant architectural differences between PostSharp and Metalama from a theoretical perspective.
<xref:migrating-multicasting> | Explains how to migrate PostSharp attribute multicasting to Metalama.
<xref:migrating-configuration> | Explains how to migrate PostSharp configuration files like `postsharp.config` to Metalama.

> [!div class="see-also"]
>
> * <xref:conceptual>
> * <xref:aspects>
> * <xref:fabrics-adding-aspects>
