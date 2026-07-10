---
uid: build-from-source
level: 200
summary: "This document provides instructions for building Metalama from source, including environment setup, cloning, building, testing, and using Docker."
keywords: "Metalama build, source code, clone repo, build script, local dependencies, Docker build, multi-repo build"
created-date: 2025-04-02
modified-date: 2026-07-10
---

# How to build from source

## Requirements

To build Metalama, you'll need:

- Windows 11 or Windows Server 2025
- PowerShell 7
- .NET SDK: one or more feature versions (see `Dockerfile`).
- .NET Framework 4.7.2 Targeting Pack (typically installed with Visual Studio 2022)

> [!NOTE]
> In case of doubt, look at the `Dockerfile` in each repo for precise requirements.

> [!WARNING]
> Ensure that the .NET SDK version matches the _feature_ version specified in the `Dockerfile` file of each repository. The `rollForward` option is set to `patch`, so only patch-level updates are allowed. Using an incorrect SDK version will result in build failures.

## Checking out source code

### 1. Clone the repo with symbolic links

Metalama uses symbolic links for `.editorconfig`. Ensure you enable symbolic links when cloning the repo:

```powershell
git clone --config core.symlinks=true https://github.com/metalama/Metalama.git
```

If you encounter numerous formatting warnings during the build, it indicates that symbolic links are not properly enabled. To resolve this, enable symbolic links, delete `.editorconfig`, and execute `git reset --hard`.

### 2. Check out the right branch

For each `YYYY.N` version, there are two branches with different purposes:

- The _release branch_ (e.g., `release/YYYY.N`) corresponds to the last deployed build for this version. This is also the default branch and usually the one you want to check out. In this branch, NuGet dependencies point to the versions that have been pushed to NuGet.org for this specific version.

- The _development branch_ (e.g., `develop/YYYY.N` or `dev/YYYY.N`) contains work in progress. NuGet dependencies in these branches are inconsistent; they typically point to the _previous_ release branch. These branches must be built using a cross-repo local build, as documented below.

For more details, see our [branching strategy](xref:branching).

## Performing a local (development) build

To build Metalama, run the following script in PowerShell:

```powershell
./Build.ps1 build
```
The packages will be placed in the `artifacts/publish/private` directory.

This command creates _development builds_ intended for use on your development machine only. Each time you run `./Build.ps1 build`, a new package version number is generated.

There are three build configurations, which you can specify using the `-c` command-line option:
- `Debug`
- `Release`
- `Public`: A release build with the following differences:
    - The version is _not_ suffixed with a unique build number; it matches the version specified in `eng/MainVersion.props`.
    - Binaries are signed. If you do not have access to the signing server, use the `--no-sign` option.
    - XML documentation files exclude internal APIs.

## Consuming local builds

After successfully creating a local build, you can use it in any project as follows:

1. Add the following code to your `Directory.Build.props` file:

    ```xml
    <Import Path="path/to/Metalama/Metalama.Imports.props" />
    ```

2. Use the `$(MetalamaVersion)` property to reference the version number of any package produced by this repository:

    ```xml
    <PackageReference Include="Metalama.Framework" Version="$(MetalamaVersion)" />
    ```

3. Run `dotnet restore` after completing a new local build.

Each time you run `./Build.ps1 build`, a new version number is generated, so you do not need to clear the cache or restart your IDE.

If you are using [package source mapping](https://learn.microsoft.com/en-us/nuget/consume-packages/package-source-mapping), you also use to add `path/to/Metalama/artifacts/publish/private` as a local source, and configure mapping,.

## Running tests

To execute tests, run the following script in PowerShell:

```powershell
./Build.ps1 test
```

## Performing a local multi-repo build

Metalama consists of several repositories with dependencies between them. When building a repository, you'll want to build downstream repositories as well.

Metalama uses [PostSharp.Engineering](https://github.com/postsharp/PostSharp.Engineering), a custom multi-repo build front-end, to manage cross-repo dependencies.

By default, dependency artifacts are downloaded from NuGet for the last build of the chosen version. When performing a multi-repo local build, you need to override the dependency source from NuGet to `local`.

Here's how to proceed:

### 1. Check out all repositories in the same parent directory

For the multi-repo build to work, check out all required Metalama repositories under the same parent directory. For instance:

- `c:\src\Metalama-2025.1\Metalama.Compiler`
- `c:\src\Metalama-2025.1\Metalama`
- `c:\src\Metalama-2025.1\Metalama.Samples`

### 2. Listing the dependencies

To list the dependencies of a repo, execute:

```powershell
./Build.ps1 dependencies list
```

For instance, the `Metalama` repo has two dependencies:

1. `PostSharp.Engineering`
2. `Metalama.Compiler`

### 3. Change the dependency source to `local`

Execute the following command:

```powershell
./Build.ps1 dependencies set local <id>
```

where `<id>` is the name or the index of the dependency.

For example, if you have a custom build of `Metalama.Compiler` and want to build the `Metalama` repo, do this in the `Metalama` repo:

```powershell
./Build.ps1 dependencies set local Metalama.Compiler
```

### 4. Repeat for each repository

You need to repeat this process for all repositories you want to build, starting from the root repository and iterating with the next level of dependencies:

1. Configure local dependencies using `./Build.ps1 dependencies set local`.
2. Build this repo using `./Build.ps1 build`.
3. Proceed to the next repository.

Specifically, you should process the repositories in the following order:

1. `Metalama.Compiler`
2. `Metalama`
3. `Metalama.Premium` (requires an enterprise subscription)
4. `Metalama.Samples`
5. `Metalama.Documentation`

## Building with Docker

We use Docker for continuous integration builds.

The host must be an AMD64 device with Windows 11 or Windows Server 2025, and Docker must be configured with Windows Containers. Using Hyper-V isolation is not recommended for performance, and untested.

To build on Docker, use the `DockerBuild.ps1` script, which acts as a wrapper of `Build.ps1`. For instance:

```powershell
.\DockerBuild.ps1 build
```

To start an interactive PowerShell prompt inside Docker, use:

```powershell
.\DockerBuild.ps1 -Interactive
```
