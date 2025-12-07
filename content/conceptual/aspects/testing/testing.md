---
uid: testing
level: 300
summary: "The document outlines three strategies for testing aspects: snapshot tests, run-time tests, and compile-time unit tests, each serving different purposes and scenarios."
keywords: "aspect testing, snapshot testing, run-time tests, unit tests, code transformation tests, error reporting tests, Xunit, testing framework, Metalama"
created-date: 2023-01-26
modified-date: 2025-12-07
---

# Testing aspects

Three complementary strategies are available for testing aspects. Snapshot testing should provide sufficient coverage for most scenarios.

<table>
    <tr>
        <th>Article</th>
        <th>Description</th>
    </tr>
    <tr>
        <td>
            <xref:aspect-testing>
        </td>
        <td>
            <strong>Snapshot testing.</strong> Verifies that aspects transform code or report errors and warnings as expected by comparing actual output against stored baseline files. The transformed code isn't executed.
        </td>
    </tr>
    <tr>
        <td>
            <xref:run-time-testing>
        </td>
        <td>
            <strong>Run-time testing.</strong> Verifies the run-time behavior of aspects. Apply an aspect to test target code and execute the transformed code in a unit test. Use xUnit or any other testing framework.
        </td>
    </tr>
    <tr>
        <td>
            <xref:compile-time-testing>
        </td>
        <td>
            <strong>Compile-time unit testing.</strong> Traditional unit tests of the compile-time helper logic used by aspects. The aspects themselves aren't executed.
        </td>
    </tr>
<tr>
    <td>
        <xref:debugging-aspects>
    </td>
    <td>
        This article describes how to debug the compile-time logic of aspects and templates.
    </td>
</tr>
<tr>
    <td>
        <xref:diff-tool>
    </td>
    <td>
        This article explains how to configure the external diff tool used when aspect tests fail.
    </td>
</tr>
</table>

> [!div class="see-also"]
> <xref:aspects>
> <xref:Metalama.Testing.AspectTesting>
> <xref:Metalama.Testing.UnitTesting>
