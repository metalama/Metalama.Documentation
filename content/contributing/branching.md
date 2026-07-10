---
uid: branching
level: 200
summary: "The document outlines Metalama's Git flow strategy involving concurrent version branches, continuous integration, and specific branch naming conventions."
keywords: "Git flow strategy, version branches, continuous integration, branch naming conventions, version control, Metalama, .NET, TeamCity, versioning"
created-date: 2025-04-02
modified-date: 2026-07-10
---

# Branching strategy

## Our Git flow

* We don't use the `master` or `main` branches.
* We generally work on three versions concurrently, numbered `YYYY.N`. Typically, one is stable and maintained, another is `rc`, and the third is `preview`.
* You should typically check out the `release/YYYY.N` branch.
* Our continuous integration branches are `develop/YYYY.N`. These often depend on unpublished build artifacts of dependencies, so they _cannot_ be easily built by the public unless the dependencies are built locally. Occasionally, our `develop/YYYY.N` builds may be broken.
* When we publish artifacts (for instance, to `nuget.org`):
  - We update the version of package references to those just uploaded to `nuget.org`.
  - We mark the released commit with the precise package version, e.g., `/release/2023.4.1-preview`.
  - We merge the `develop/YYYY.N` branch into `release/YYYY.N`.
* We work on branches named `topic/YYYY.N/whatever` and generally do PRs to `develop/YYYY.N`.
* After any merge to an "old" `develop/YYYY.N`, the "old" `develop/YYYY.N` is automatically merged into the newer `develop/YYYY.N+1`. A merge commit named `merge/YYYY.N+1/commit-123456` is automatically created, tested, merged if possible, and then deleted.
* We use a private TeamCity service for our continuous integration.

### Illustration

The following diagram illustrates our workflow. It shows two public builds, `2023.4.1-preview` and `2023.4.2-preview`, each including two bug fixes.

<!-- The "commit" before the first "merge develop/2023.4" is a workaround for
     https://github.com/mermaid-js/mermaid/issues/5898 and should be removed when fixed. -->

```mermaid

%%{init: { 'gitGraph': { 'mainBranchName':'develop/2023.4', 'mainBranchOrder': 1, 'showCommitLabel': false }} }%%

gitGraph:
    commit
    branch release/2023.4 order:0
    branch topic/2023.4/1234-bug-1 order:1
    checkout topic/2023.4/1234-bug-1
    commit
    commit
    checkout develop/2023.4
    merge topic/2023.4/1234-bug-1
    branch topic/2023.4/1235-bug-2 order:2
    checkout topic/2023.4/1235-bug-2
    commit
    commit
    checkout develop/2023.4
    merge topic/2023.4/1235-bug-2 tag:"release/2023.4.1-preview" type:HIGHLIGHT
    checkout release/2023.4
    commit
    merge develop/2023.4
    branch topic/2023.4/1236-bug-3 order:3
    checkout topic/2023.4/1236-bug-3
    commit
    commit
    checkout develop/2023.4
    merge topic/2023.4/1236-bug-3
    branch topic/2023.4/1237-bug-4 order:4
    checkout topic/2023.4/1237-bug-4
    commit
    commit
    checkout develop/2023.4
    merge topic/2023.4/1237-bug-4 tag:"release/2023.4.2-preview" type:HIGHLIGHT
    checkout release/2023.4
    merge develop/2023.4
    commit
```
