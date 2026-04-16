---
uid: divorcing
level: 200
summary: "The document explains the process of separating from the Metalama software framework, using the 'metalama divorce' command. It outlines the potential drawbacks and a seven-step procedure to ensure a smooth transition."
keywords: "metalama divorce, separating from Metalama, inject generated code, manual maintenance, boilerplate code, .NET, remove references"
created-date: 2023-03-31
modified-date: 2026-04-13
---

# Divorcing from Metalama

Metalama's _Divorce_ feature injects all generated code back into your source files and strips out the framework. You get a codebase that compiles under the stock Microsoft compiler. Metalama gets its stuff and leaves.

## Why a Divorce feature

A few years ago, while pitching PostSharp to an Israeli prospect, they asked if adopting the framework was, like a Catholic marriage, _til death do us part_. We didn't have a great answer. You _could_ remove PostSharp, but you'd have to rewrite every line of generated code by hand, undoing years of accumulated time savings. 

So we built Metalama to be the considerate partner. Want out? Run `metalama divorce`. It injects the generated code back into your source, disables Metalama in your projects, and steps aside. A few hours later you're compiling with the plain Microsoft compiler as if nothing happened.

You will, of course, lose everything that made the relationship work: deterministically auto-generated boilerplate, compile-time architecture validation, and the quiet confidence that your cross-cutting concerns are handled. You'll be writing that code by hand again. You might also use an AI: faster, but neither deterministic nor infallible. But Metalama isn't the kind of partner that makes you fight for custody of your own source files.

Before you file the papers, though: if something isn't working, talk to the Metalama team. Sometimes the problem has a fix, or Metalama can be extended to address it. We'd rather improve the framework than wave you goodbye. Think of us as the couples therapist who happens to know the framework's source code.

## Step 0. Consider your decision carefully

Despite Metalama's best efforts, no divorce is truly painless.

The `metalama divorce` command injects a large amount of boilerplate into your source code. Before you proceed, weigh these consequences:

* You'll now have to maintain this boilerplate code manually.
* Metalama doesn't always generate code that a human would write. Your codebase may look non-idiomatic after the divorce. You can preview what Metalama does with your code using the feature described in <xref:understanding-your-code-with-aspects>.
* The changes produce a large commit that will be difficult to merge if colleagues are working on other branches.
* Returning to Metalama after the divorce can be even more painful because you would need to remove the boilerplate manually, unless you can easily revert the divorce commit.

## Step 1. Prepare your code

Format your code to your preferred standard using a tool like [dotnet format](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format) or the [Clean Up](https://www.jetbrains.com/help/rider/Code_Cleanup__Index.html) feature of ReSharper or Rider. Metalama's generated code will _not_ respect your formatting rules, and you'll reformat again after the divorce, so starting from a clean baseline keeps the diff readable.

> [!WARNING]
> The pre-divorce formatting pass may surface pre-existing issues in your codebase that are unrelated to the divorce itself. For example, `dotnet format` code-fix analyzers can rewrite API calls in ways that don't compile without additional `using` directives. If this happens, fix the formatting issues first, verify the build still passes, and then proceed.

Ensure all your unit tests are successful.

## Step 2. Commit your code

Ensure your code is committed. Create a separate branch for the divorce and check it out.

## Step 3. Build your code with special flags

1. Open a terminal window.

2. **Clean all `obj` and `bin` directories** throughout your solution before proceeding. The divorce tool copies whatever `.transformed` files it finds under each project's `obj/` directory. If a prior build populated `obj/` with output produced under different preprocessor constants or without `MetalamaFormatOutput`, those stale files will be copied back and the divorced code may fail to parse.

   ```powershell
   Get-ChildItem -Path . -Recurse -Force -Directory |
       Where-Object { $_.Name -in 'obj', 'bin' } |
       Remove-Item -Recurse -Force
   ```

3. Define the following environment variables:

   ```powershell
   $env:MetalamaEmitCompilerTransformedFiles="true"
   $env:MetalamaFormatOutput="true"
   ```

   Note that the syntax differs if you're not using PowerShell. You can also define these properties in `Directory.Build.props`, but make sure they apply to all projects using Metalama.

4. Rebuild _all_ your projects. Don't miss any! Your build may take longer than usual due to the `MetalamaFormatOutput` property.

Building the projects with these two properties will write the transformed code files to disk in the `transformed` directory, located under the `obj` directory of each project.

## Step 4. Execute the divorce command

Install the `metalama` tool as described in <xref:dotnet-tool>.

Then, execute the following command from the root directory of your repository.

```powershell
metalama divorce
```

This command will:

* Copy all files under the `obj/**/transformed` directory back to their original location in the source code.
* Set the `<MetalamaEnabled>` MSBuild property to `false` in every `.csproj` file, so subsequent builds use the standard Microsoft compiler instead of Metalama.

## Step 5. Reformat your code

We suggest you format your code again using the same tool and parameters as in Step 1.

## Step 6. Commit

Review the changes in your repository and commit them to your new branch. Do not merge yet, you're not done!

## Step 7. Remove any reference to Metalama

At this point, your code base no longer requires processing by the Metalama compiler. The `metalama divorce` command has already set `MetalamaEnabled` to `false` in every `.csproj`. However, your code base still contains references to the Metalama libraries. Removing them is tedious but straightforward.

Currently, Metalama doesn't provide a way to automatically remove fabrics and aspect custom attributes from your code. Therefore, we recommend:

* Editing all aspects to turn them into plain custom attributes,
* Removing all fabrics,
* Removing Metalama NuGet package references from your projects.

## PowerShell script

The steps above, except the last one, are summarized in the following script.

```powershell
# Run git status and capture the output
$gitStatus = $(git status --porcelain)

# Check if the repo has uncommitted changes
if (-not [string]::IsNullOrWhiteSpace($gitStatus)) {
    throw "Uncommitted changes detected. Please commit or stash your changes."
}

# Create a new branch
$currentTimestamp = Get-Date -Format "yyyyMMdd-HHmmss"
$branchName = "divorce-$currentTimestamp"
git checkout -b $branchName

# Format code
dotnet format

# Commit
git commit -a -m "Formatting the code before Metalama divorce."

# Clean obj/bin to avoid stale transformed files
Get-ChildItem -Path . -Recurse -Force -Directory |
    Where-Object { $_.Name -in 'obj', 'bin' } |
    Remove-Item -Recurse -Force

# Build with transformed-files output
$env:MetalamaEmitCompilerTransformedFiles = "true"
$env:MetalamaFormatOutput = "true"
dotnet build /t:rebuild
Remove-Item Env:MetalamaEmitCompilerTransformedFiles -ErrorAction SilentlyContinue
Remove-Item Env:MetalamaFormatOutput -ErrorAction SilentlyContinue

# Write generated code back to the source code
metalama divorce

# Format
dotnet format

# Commit the divorce
git commit -a -m "Metalama divorce: inject transformed code."

# Verify the build succeeds with the stock compiler
dotnet build /t:rebuild
```

> [!div class="see-also"]
> <xref:understanding-your-code-with-aspects>
> <xref:dotnet-tool>
> <xref:msbuild-properties>
