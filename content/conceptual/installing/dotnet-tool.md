---
uid: dotnet-tool
level: 200
summary: "This document provides instructions on how to install the Metalama Command Line Tool, which offers several features for managing the Metalama software."
keywords: "command line tool, dotnet tool, configuration, license, processes, temporary files"
created-date: 2023-02-17
modified-date: 2026-10-28
---

# Installing the Metalama command line tool

The Metalama command line tool provides the following features:

* Installing a license or switching to Metalama Free
* Configuring various settings
* Terminating Metalama processes
* Cleaning up Metalama temporary files
* Previewing the transformed code of a few files without building the project (see <xref:previewing-transformed-code>)

To install the Metalama command line tool, run the following command:

```powershell
dotnet tool install -g metalama.tool
```

The Metalama command line tool is now accessible through the `metalama` command.

> [!div class="see-also"]
> <xref:installing>
> <xref:configuration>
> <xref:register-license>
> <xref:creating-logs>
> <xref:divorcing>
> <xref:previewing-transformed-code>
