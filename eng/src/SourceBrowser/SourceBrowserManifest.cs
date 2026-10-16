// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System.Collections.Generic;

namespace BuildMetalamaDocumentation.SourceBrowser;

// The manifest of the source browser, published as 'source/manifest.json' in the documentation archive and read by
// HelpBrowser. All paths are relative and use forward slashes. The manifest contains no local path.

/// <summary>
/// The root of <c>source/manifest.json</c>.
/// </summary>
/// <param name="Version">The version of the manifest format.</param>
/// <param name="Directories">The published source directories, each once, whatever the number of articles that show it.</param>
/// <param name="Mounts">The nodes that show the directories in the navigation, under the articles that declare them.</param>
internal sealed record SourceBrowserManifest(
    int Version,
    IReadOnlyList<SourceBrowserDirectory> Directories,
    IReadOnlyList<SourceBrowserMount> Mounts );

/// <summary>
/// A published source directory.
/// </summary>
/// <param name="Path">The path of the directory in the site, for instance <c>source/samples/log/log-3</c>.</param>
/// <param name="Title">The display name of the directory, i.e. the name of its project if it has exactly one.</param>
/// <param name="GitHubUrl">The URL of the directory on GitHub.</param>
/// <param name="Files">The files of the directory. A subdirectory that is published as a directory of its own is excluded.</param>
internal sealed record SourceBrowserDirectory(
    string Path,
    string Title,
    string? GitHubUrl,
    IReadOnlyList<SourceBrowserFile> Files );

/// <summary>
/// A node of the navigation that shows a source directory. It is declared by the <c>browse-source</c> front matter key
/// of an article.
/// </summary>
/// <param name="Topic">The uid of the article under which the node is shown. It is set only for top-level nodes; a child
/// node is shown under its parent node.</param>
/// <param name="Title">The title of the node, e.g. <c>Source code</c> or <c>Tests</c>.</param>
/// <param name="Directory">The path of the directory in the site, i.e. <see cref="SourceBrowserDirectory.Path"/>.</param>
/// <param name="Children">The child nodes, shown under this node.</param>
internal sealed record SourceBrowserMount(
    string? Topic,
    string Title,
    string Directory,
    IReadOnlyList<SourceBrowserMount> Children );

/// <summary>
/// A file of a published source directory.
/// </summary>
/// <param name="Path">The path of the file relative to the directory. The raw file is published at this path.</param>
/// <param name="Role">The role of the file: <c>aspect</c>, <c>target</c> or <c>other</c>.</param>
/// <param name="GitHubUrl">The URL of the file on GitHub.</param>
/// <param name="SourceHtml">The path, relative to the directory, of the syntax-highlighted HTML of the source code, if any.</param>
/// <param name="TransformedHtml">The path, relative to the directory, of the syntax-highlighted HTML of the transformed code,
/// if any. It is omitted when the transformed code equals the source code.</param>
/// <param name="Topics">The uids of the articles that include the file.</param>
/// <param name="TransformedPath">The path, relative to the directory, of the expected transformed code of an aspect test,
/// i.e. <c>X.t.cs</c> for <c>X.cs</c>. That file is not listed separately: it is shown as the transformed code of this file.</param>
internal sealed record SourceBrowserFile(
    string Path,
    string Role,
    string? GitHubUrl,
    string? SourceHtml,
    string? TransformedHtml,
    IReadOnlyList<string> Topics,
    string? TransformedPath = null );
