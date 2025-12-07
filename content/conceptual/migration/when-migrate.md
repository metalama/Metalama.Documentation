---
uid: when-migrate
summary: "The document provides guidance on when to migrate from PostSharp to Metalama, highlighting potential issues, the option of concurrent use, and specific circumstances for considering migration."
keywords: "migration, PostSharp, Metalama"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# When to migrate from PostSharp to Metalama

Before deciding to migrate your code from PostSharp to Metalama, evaluate whether it's the right time to do so. Since PostSharp will continue to be maintained for several more years, there may not be an immediate need to switch to Metalama.

## Avoid mixing PostSharp and Metalama in the same project

While it's theoretically possible to use both PostSharp and Metalama in the same project, we don't recommend it:

* Both Metalama and PostSharp introduce helper methods and properties—methods without an equivalent in the source code. Since PostSharp operates _after_ Metalama, it recognizes these helper declarations and the aspects treat them as user code, potentially causing confusion and errors.
* All Metalama aspects are applied before any PostSharp aspect, simply because Metalama operates before PostSharp. This constrains the order in which aspects can be applied.
* We haven't tested PostSharp and Metalama together and won't investigate or resolve issues that may arise from their combined use. This scenario isn't _supported_.

## Migrating isn't an all-or-nothing decision

Teams or companies can use both PostSharp and Metalama concurrently. There's no need for a company-wide decision to initiate the migration.

You can continue using PostSharp for one product while migrating to Metalama for another. This approach may be viable as long as these products don't have dependencies that would result in a single C# project using both Metalama and PostSharp.

## When to migrate to Metalama

Consider migrating to Metalama if:

* Your project depends on a platform that won't support PostSharp (for instance, an ARM64 build environment or WinUI projects), or
* The benefits outweigh the effort required for migration. For more details, see <xref:benefits-over-postsharp>.

Don't migrate to Metalama at this time if:

* Your project depends on a platform that's supported by PostSharp but _not_ by Metalama (for example, a pre-2020 platform):

  * Visual Studio 2019 or earlier
  * .NET Standard 1.6 or earlier
  * .NET Framework 4.6 or earlier
  * Visual Basic (Metalama is available for C# projects only)

* Your project depends on PostSharp features that haven't yet been ported to Metalama. For more details, see <xref:migration-feature-status>.
* Your company has a large team working on a business-critical project with a tight deadline and prefers the tried-and-tested PostSharp (2008) over the relatively new Metalama (2023).

> [!div class="see-also"]
>
> * <xref:migration>
> * <xref:benefits-over-postsharp>
> * <xref:migration-feature-status>
> * <xref:migrating-aspects>
