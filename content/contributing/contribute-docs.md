---
uid: contribute-docs
level: 200
summary: "This document guides on contributing to Metalama's documentation using DocFx, including cloning, building, authoring, updating, and preparing a PR."
keywords: "DocFx, Metalama, documentation, authoring sample code, Metalama.Testing.AspectTesting, authoring documentation article, update documentation, prepare PR"
created-date: 2025-04-02
modified-date: 2026-07-10
---

# How to contribute documentation

Metalama uses [DocFx](https://dotnet.github.io/docfx/index.html) to build its documentation.

## 1. Clone the repo

```powershell
git clone --config core.symlinks=true https://github.com/metalama/Metalama.Documentation
```

## 2. Build the project

We recommend building the entire repository. This will automatically restore and build all dependencies.

From PowerShell, execute:

```powershell
./Build.ps1 build
```

## 3. Authoring sample code

Most samples are created using the [Metalama.Testing.AspectTesting](xref:aspect-testing) framework, where each example consists of a group of files (typically a source file and a file with the expected output).

First, open the solution and the project in your IDE:
- All sample code projects are located under the `code/` directory.
- Samples using the `Metalama.Testing.AspectTesting` framework are all in the `Metalama.Documentation.Snippets.ProjectBased.sln` solution.
- Samples that only depend on `Metalama.Framework` are in the `Metalama.Documentation.SampleCode.AspectFramework.csproj` project.

You can now add a new sample as described in the [Metalama.Testing.AspectTesting](xref:aspect-testing) documentation or by examining other samples.

When you build the documentation projects, the build script automatically generates syntax-highlighted HTML files, including diff files, from this code.

## 4. Authoring a documentation article

Documentation articles must be written in Markdown and can be found under the `content/` directory. We typically use Visual Studio Code.

Follow these recommendations:

- Use the `<xref:>` tag to include references to code. For details, see [Links and Cross-References](https://dotnet.github.io/docfx/docs/links-and-cross-references.html) in the DocFx documentation. You can search for the proper reference string in `*.yml` files under the `artifacts/api` directory.
- Include a YAML front matter with the following information:
    - `uid`: the unique identifier of your article
    - `level`: the difficulty level
    - `summary`: the SEO summary
    - `keywords`: the SEO keywords
    - `created-date` and `modified-date`: the date of first creation and last modification

    For example:

    ```YAML
    uid: validation
    level: 200
    summary: "The document discusses verifying source code against architecture, design patterns, and team conventions, emphasizing the benefits of immediate feedback, smoother code reviews, team alignment, reduced complexity, and architecture erosion prevention."
    keywords: "code verification, architecture validation, design patterns, team conventions, immediate feedback, smoother code reviews, team alignment, reduced complexity, architecture erosion prevention, Metalama"
    created-date: 2023-01-26
    modified-date: 2024-08-04
    ```

- Include your article in the table of contents by editing the appropriate `toc.yml` file.
- Also, add your article to the mini-TOC of the parent article.
- If your article includes an image, add it to the same directory as the article.

## 5. Update the documentation

Refresh the documentation output by running the following command in PowerShell:

```powershell
./update-html.ps1
```

Ensure you did not introduce any warnings.

## 6. Preview the documentation

Unfortunately, the web engine that renders the documentation on https://doc.metalama.net is not publicly available yet.

To preview the documentation, you can still use a browser to open `artifacts/site/index.html` and then navigate to your article.

If you need changes, iterate to step 3 or 4.

## 7. Execute a complete build

When you are satisfied with your modifications, run a new complete build of the documentation:

```powershell
./Build.ps1 build
```

Ensure the build is successful.

## 8. Prepare a PR

Congratulations! You can now prepare a PR against the `develop/YYYY.N` branch.
