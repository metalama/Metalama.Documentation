---
uid: migrating-aspects
summary: "The document provides a step-by-step guide on migrating custom aspects from PostSharp to Metalama, including learning Metalama, assessing aspects' importance, verifying migration timing and project state, changing package references, creating a separate project for aspect tests, implementing individual aspects, migrating multicasting usages and configurations, and final project cleanup. "
keywords: "migrating custom aspects, PostSharp to Metalama"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Migrating custom aspects to Metalama

## Step 0: Learn Metalama in a sandbox project

Before porting your PostSharp aspects to Metalama, familiarize yourself with the new product by using it in a new project. Ideally, write a prototype of aspects in a sandbox project without PostSharp. While migrating aspects, you may not be able to run your application or all your unit tests, so learning in a sandbox project is advisable.

## Step 1: Create a list of aspects and assess their importance

First, identify all used aspects across all projects.

Next, assess the importance of these aspects:

1. Aspects required for unit tests to succeed,
2. Aspects required for the application to run,
3. Aspects required in production but not in a development environment, such as logging, profiling, or caching.

After assessing, sort the aspects by importance. This list will guide you on how to implement the aspects.

### Identify uses of multicasting

Determine if _attribute multicasting_ is used for each aspect, and how it's used. All PostSharp aspects implicitly inherit from the `MulticastAttribute`, meaning all aspects _can_ be multicast, but not all necessarily are. To identify which aspects use the multicasting feature, perform a "Find in Files" for the following substrings: `AttributeTarget` or `AttributeExclude`.

If an aspect is multicast only from assembly-level custom attributes, your Metalama aspect doesn't have to implement multicasting. Using fabrics is a more elegant approach than assembly-level multicasting. See <xref:fabrics>. However, if the source code uses multicasting from classes or structs, your Metalama aspect must implement multicasting.

## Step 2: Verify that it's a good time to migrate

Refer to <xref:when-migrate> and ensure that Metalama supports the target frameworks of your projects. Unless the projects still target obsolete platforms, they are likely to be supported.

Check <xref:migration-feature-status> to see if your projects use a PostSharp feature that hasn't been ported to Metalama yet. If your project uses ready-made aspects from the `PostSharp.Patterns` namespace, verify that these aspects have been ported.

## Step 3: Verify that your projects are in a clean state

Before starting the migration, ensure that your projects are in a clean state:

* Create a new branch for this task in your source repository.
* Ensure that the projects build without error. If possible, resolve any warnings in your code.
* Confirm that all unit tests are successful.
* Once your projects are free of errors, warnings, and failed tests, commit and push your branch.

## Step 4: Change PostSharp package references to Metalama.Migration and check errors

Replace all references to the `PostSharp` package with references to the `Metalama.Migration` package across all projects. This can likely be done with a "Replace in Files" operation. However, if you use `packages.config`, consider migrating your projects to the new `PackageReference` format. If you can't, use the NuGet Package Manager UI to uninstall the `PostSharp` package and install the `Metalama.Migration` package.

The `Metalama.Migration` package contains the public API of PostSharp, but not its implementation. Instead, it contains `[Obsolete]` annotations with indications about the equivalent class or method in Metalama.

Building these projects at this point will likely result in numerous warnings.

If your code uses APIs that haven't been ported to Metalama, you may also encounter errors. If you see errors, consider possible workarounds. If none are available, you may need to delay the migration effort.

Don't attempt to run your application or your unit tests at this point. They won't function until you port all critical aspects.

## Step 5: Create a separate project for aspect tests

Since your current test base will likely be disrupted for a while &mdash; until all your critical aspects have been ported &mdash; it's a good idea to set up an infrastructure where you can test your aspects individually.

For more details, see <xref:aspect-testing>.

## Step 6: Implement the individual aspects

Using the list you created in step 1, start with the most essential aspects.

Metalama isn't just an update of PostSharp—it has a completely different architecture and approach. You'll need to rewrite each aspect from scratch.

To determine the Metalama equivalent of any PostSharp API, refer to the `[Obsolete]` warnings reported by the `Metalama.Migration` package on your code. We haven't included a tutorial on porting a specific aspect to Metalama here because, if you've completed Step 0 as suggested, you should already be familiar with this process. As mentioned earlier, learn Metalama _before_ undertaking the actual migration work.

We strongly recommend writing aspect tests for each aspect you're implementing.

If you identified in Step 1 that the aspect should support multicasting, refer to <xref:migrating-multicasting>.

## Step 7: Migrate usages of multicasting

The best way to migrate assembly multicasting is to use a project fabric as described in <xref:fabrics-adding-aspects>.

For type-level multicasting, if you've built multicasting into your Metalama aspects as described in <xref:migrating-multicasting>, replacing the namespace `PostSharp.Extensibility` with `Metalama.Extensions.Multicast` using a "Replace in Files" operation should suffice.

## Step 8: Migrate configuration

If you have configuration or multicasting in a PostSharp file like `postsharp.config` or `MyProject.psproj`, they should be migrated to project fabrics as described in <xref:fabrics-configuration>.

For more details, see <xref:migrating-configuration>.

## Step 9: Last project cleanup

At this point, we recommend addressing all warnings.

If your Metalama aspects have been correctly implemented, all your tests should now pass and the application should run as before.

Once this is the case, replace the reference to the `Metalama.Migration` package with `Metalama.Framework`.

You've completed the migration.

> [!div class="see-also"]
>
> * <xref:benefits-over-postsharp>
> * <xref:differences-from-postsharp>
> * <xref:migration-feature-status>
> * <xref:migrating-configuration>
> * <xref:migrating-inpc>
> * <xref:fabrics>
> * <xref:fabrics-adding-aspects>
> * <xref:fabrics-configuration>
> * <xref:aspect-testing>
> * <xref:migrating-multicasting>
