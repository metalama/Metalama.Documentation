---
uid: testing
level: 300
summary: "The document outlines three strategies for testing aspects: compile-time tests, run-time tests, and traditional unit tests, each serving different purposes and scenarios."
keywords: "aspect testing, compile-time tests, run-time tests, unit tests, code transformation tests, error reporting tests, Xunit, testing framework, Metalama"
created-date: 2023-01-26
modified-date: 2025-11-30
---

# Testing aspects

Three complementary strategies are available to test your aspects. Aspect testing should provide sufficient coverage for the most common scenarios.

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
            These tests verify that aspects transform code or report errors and warnings as expected. The transformed code is not executed.
        </td>
    </tr>
    <tr>
        <td>
            <xref:run-time-testing>
        </td>
        <td>
            These tests verify the run-time behavior of aspects. Apply your aspect to test target code and execute the transformed code in a unit test. Use xUnit or any other testing framework.
        </td>
    </tr>
    <tr>
        <td>
            <xref:compile-time-testing>
        </td>
        <td>
            These tests are traditional unit tests of the compile-time logic used by aspects. The aspects themselves are not executed.
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


