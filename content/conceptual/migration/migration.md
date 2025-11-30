---
uid: migration
summary: "The document provides guidance on migrating projects from PostSharp to its successor, Metalama, including rewriting aspects, maintaining business code, and various articles to assist the migration process."
keywords: "migration, PostSharp, Metalama, rewrite aspects"
created-date: 2023-01-26
modified-date: 2024-08-04
---

# Migrating from PostSharp

You may want to migrate some or all of your projects from PostSharp to Metalama. If you decide to proceed, this chapter will assist you.

When we conceptualized Metalama as the successor to PostSharp, we elected to break backward compatibility. After all, the PostSharp Framework was designed during the years of C# 2.0 and .NET Framework 2.0 (2004-2010), and we've maintained backward compatibility since then. We couldn't leverage the new .NET stack (for instance, C# 11, .NET 6, and Roslyn) by adhering strictly to our 2010 API. Thus, we opted for a comprehensive redesign of the concepts and APIs.

However, we understand that many customers have codebases ranging from tens of thousands to millions of lines of code that utilize PostSharp. That's why we ensured that these customers wouldn't need to port these large codebases.

Consequently, we made the following decisions:

* You will need to completely rewrite your _aspects_.
* The _business code_ that merely _uses_ the aspects _should not typically require any changes_, except for find-and-replace-in-files operations and replacements of namespace imports.

We hope this is a satisfactory compromise.

## In this chapter

Article | Description
-|-
<xref:migrating-aspects> | This article provides a comprehensive step-by-step guide for your migration project and refers to other articles in this chapter.
<xref:benefits-over-postsharp> | This article outlines the advantages of Metalama over PostSharp.
<xref:when-migrate> | This article offers a few points to consider before migrating your aspects to Metalama. Ensure you read this article before making any decisions.
<xref:migration-feature-status> | This article details the status of PostSharp features in Metalama.
<xref:differences-from-postsharp> | This article discusses the significant architectural differences between PostSharp and Metalama from a theoretical perspective.
<xref:migrating-multicasting> | This article guides you on how to migrate PostSharp attribute multicasting to Metalama.
<xref:migrating-configuration> | This article instructs you on how to migrate PostSharp configuration files like `postsharp.config` to Metalama.
