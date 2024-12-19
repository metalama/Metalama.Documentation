---
created-date: 2023-02-02
modified-date: 2024-04-10
---
<p align="center">
<img width="450" src="https://github.com/postsharp/Metalama/raw/master/images/metalama-by-postsharp.svg" alt="Metalama logo" />
</p>

# Metalama.Documentation

This repository contains the documentation for Metalama. It is recommended to read it online at https://doc.metalama.net.

The code snippets in this documentation are located under the `code` subdirectory and are fully unit testable. Other examples are derived from the [Metalama.Samples](https://github.com/postsharp/Metalama.Samples) and [Metalama.Community](https://github.com/postsharp/Metalama.Community) repositories.

## Building everything

To build everything, use the following command:

```powershell
.\Build.ps1 build
```


## Our Markdown extensions

### metalama-test

This markup includes an aspect test in a tab group. The target code is displayed as a side-by-side diff.

```
[!metalama-test <path> [tabs="<tabs>"] ]
```

where:

* `<path>` represents a relative path, typically starting with `~`, where `~` is replaced by the root of the current repository.
* `<tabs>` is a comma-separated list of one or more of the following values:

    * `aspect`
    * `target`
    * `transformed`
    * `output`

### metalama-compare

This markup displays source and transformed code side by side as a diff.

```
[!metalama-compare <path>]
```

where:

* `<path>` represents a relative path, typically starting with `~`, where `~` is replaced by the root of the current repository.

### metalama-file

This markup includes a source file or a portion of it.

```
[!metalama-file <path> [transformed] [marker='foo'] [member='<member>']]
```

where:

* `<path>` represents a relative path, typically starting with `~`, where `~` is replaced by the root of the current repository.
* `transformed` indicates that the transformed code should be displayed instead of the source code.
* `marker` indicates that only the code between lines with comments `/*<Foo>*/` and `/*</Foo>*/` should be included.
* `member` indicates that only the given member (provided in the form `TypeName.MemberName` without namespace) should be included.

### metalama-files

This markup creates a tab group with several files.

```
[!metalama-files <path1> <path2> ... <path_n> [links="true|false"]]
```

where:

* `<path1>` ... `<path_n>` represent relative paths, typically starting with `~`, where `~` is replaced by the root of the current repository.
* `links` indicates whether GitHub links should be generated.

### metalama-project-buttons

This markup generates buttons that open the entire directory in GitHub.

```
[!metalama-project-buttons <path>]
```

where:

* `<path>` represents a relative path to the directory.

### metalama-vimeo

This markup embeds a Vimeo video.

```
[!metalama-vimeo <id>]
```

where:

* `<id>` represents the video ID.

