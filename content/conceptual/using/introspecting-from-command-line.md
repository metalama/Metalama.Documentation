---
uid: introspecting-from-command-line
level: 200
summary: "This document explains how to query from the command line which aspects apply to the code and what they change, with the metalama introspect command, which reads the introspection file of the last build. It is designed for AI coding agents and for environments without Visual Studio."
keywords: "metalama introspect, introspection file, mlsym, aspect instances, transformations, predecessors, command line, AI agent, MetalamaWriteIntrospectionFile"
created-date: 2026-11-13
modified-date: 2026-11-13
---

# Querying aspects from the command line

The `metalama introspect` command answers questions about the aspects of a project: which aspects apply to a declaration, what they change, what created them, and which diagnostics they reported. The command reads a file that the build writes, the _introspection file_. It never builds the project.

The command is designed for AI coding agents and for other tools that cannot use the Aspect Explorer of Visual Studio. It is also useful in other IDEs and in a terminal. When an agent reads code to which aspects apply, the source code does not show the code that runs. The agent can ask the command which aspects apply to a method before it changes the method.

To see the transformed code itself rather than a description of the aspects, see <xref:previewing-transformed-code>.

> [!NOTE]
> The command requires a project that uses Metalama 2027.0.7 or later. The feature requires no license.

## Installing the command

The `introspect` command belongs to the Metalama command line tool. To install the tool, see <xref:dotnet-tool>.

## Writing the introspection file

The build writes the introspection file only when the `MetalamaWriteIntrospectionFile` MSBuild property is `True`. The property is `False` by default. Writing the file costs time at every build, so do not set the property in a project file or in `Directory.Build.props`. Set it for the current session only, with an environment variable. MSBuild reads environment variables as properties.

To write the file and query it, follow these steps:

1. Set the environment variable in the current session.

   In PowerShell:

   ```powershell
   $env:MetalamaWriteIntrospectionFile = "True"
   ```

   In Bash:

   ```bash
   export MetalamaWriteIntrospectionFile=True
   ```

2. Build the project. A rebuild is not necessary: the property is an input of the compiler, so a normal build compiles the project again when the property changes, even when the project is already built.

   ```powershell
   dotnet build
   ```

3. Run the `metalama introspect` command, as described below.

Keep the variable set while you work. A build without the property compiles the project again and deletes the file.

The file is named after the assembly, with the `.mlsym` extension, and is written next to the assembly, for instance `bin/Debug/net10.0/MyProject.mlsym`. The `MetalamaIntrospectionFile` property changes its path. A build of each target framework writes its own file. `dotnet clean` deletes the file.

When no file exists, the command reports it and names the property. When a source file is newer than the introspection file, the command warns that the file may not describe the current source. In both cases, build the project again.

## Listing objects

The `list` command lists the objects of a collection:

```powershell
metalama introspect list aspect-instances
```

The collections are the following:

| Collection | Objects |
|------------|---------|
| `aspect-classes` | The classes of the aspects, with their aspect layers. |
| `aspect-instances` | One aspect applied to one declaration. |
| `predecessors` | What created the aspect instances: a custom attribute, a fabric, an inherited attribute, or another aspect instance for a child aspect or a required aspect. |
| `transformations` | The changes that the aspects make to the declarations: overridden members, introduced members, implemented interfaces, and the other kinds of changes. |
| `diagnostics` | The warnings and errors that the aspects reported. |
| `declarations` | The declarations that the file mentions, with what applies to them. |

The `--where` option filters the objects with a condition, and the `--select` option chooses the fields of the output. Both use the syntax of OData, the same as the `$filter` and `$select` options of a web API:

```powershell
metalama introspect list aspect-instances --where "aspectClass/shortName eq 'Log'" --select id,targetDeclaration/id
```

The `--order-by` option sorts the objects, and the `--count` option writes only their number. By default, the command writes at most 100 objects and says when the result is truncated. The `--limit` and `--all` options change this number.

To learn the fields of each collection, run the `schema` command:

```powershell
metalama introspect schema aspect-instances
```

## Showing one object

The `show` command shows one object with what relates to it. The most useful form takes a position in a source file and shows the declaration at this position, with its aspects and their transformations in order:

```powershell
metalama introspect show declaration Code.cs:40
```

The output is JSON. In the following example, a `[Log]` attribute applies an aspect that overrides the method and reports a warning. The output is abridged: the fields that repeat the project and the target frameworks, and the lists that are empty, are removed.

```json
{
  "declaration": {
    "id": "M:App.C.Add(System.Int32,System.Int32)~System.Int32",
    "kind": "Method",
    "name": "Add",
    "location": "Code.cs:40:16",
    "declaringType": { "id": "T:App.C", "kind": "Type" }
  },
  "aspectInstances": {
    "count": 1,
    "truncated": false,
    "items": [
      {
        "id": "86e0a5bdd9b3",
        "aspectClass": { "name": "App.LogAttribute", "shortName": "Log" },
        "predecessors": {
          "count": 1,
          "truncated": false,
          "items": [
            { "id": "54951598a8e4", "kind": "Attribute", "location": "Code.cs:39:6" }
          ]
        },
        "transformations": {
          "count": 1,
          "truncated": false,
          "items": [
            {
              "kind": "OverrideMember",
              "adviceKind": "OverrideMethod",
              "description": "Override the method 'C.Add(int, int)'",
              "order": 0
            }
          ]
        },
        "diagnostics": {
          "count": 1,
          "truncated": false,
          "items": [
            { "id": "APP001", "severity": "Warning", "message": "The method 'C.Add(int, int)' is logged.", "location": "Code.cs:40:16" }
          ]
        },
        "isSkipped": false
      }
    ]
  }
}
```

The `--format text` option writes the same information as text, for a person.

A position selects the innermost declaration that contains it and to which an aspect applies. A line in a method without aspects therefore selects the type of the method when an aspect applies to the type.

The `show` command also accepts a C# name, such as `OrderService.PlaceOrder`, a short aspect class name, such as `Log`, or the identifier of an object, which the `list` command gives. The collections of `show` are `declaration`, `aspect-instance`, `predecessor` and `aspect-class`.

## Options

| Option | Description |
|--------|-------------|
| `--project <path>` | The project file, or a directory that holds one. By default, the only project or solution of the current directory. |
| `--solution <path>` | The solution file, or a directory that holds one. The command then queries all the projects of the solution together. |
| `-f`, `--framework <tfm>` | Restricts the query to one target framework. |
| `-c`, `--configuration <name>` | The configuration of the build output. By default, the default configuration of each project, usually `Debug`. |
| `--format <format>` | `json`, the default, `text` for a person, or `jsonl` for one object per line. |
| `--color <when>` | Colors the JSON output: `auto`, the default, `always` or `never`. |

The `batch` command runs several queries over one read of the files, one query per line of the standard input or of the file given by `--file`.

The command returns the exit code 0 on success, 1 on an error, 2 when no introspection file exists, 3 when a selector matches several objects, and 4 when a query is not valid.

## Limitations

* The command describes the last build, not the current source. Build the project after a change before you query it.
* The file records only the declarations that aspects target or mention. A position in a declaration to which no aspect applies selects a containing declaration to which an aspect applies, or nothing.
* The command does not show the transformed code. Use the `metalama preview` command for this purpose. See <xref:previewing-transformed-code>.

## See also

> [!div class="see-also"]
> <xref:ai-agents>
> <xref:previewing-transformed-code>
> <xref:dotnet-tool>
> <xref:msbuild-properties>
> <xref:introspection>
