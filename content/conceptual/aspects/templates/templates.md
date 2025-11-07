---
uid: templates
level: 200
summary: "The document provides a guide on writing T# templates in Metalama, covering topics like template language introduction, compile-time code, dynamic code generation, and debugging."
keywords: "T# templates, Metalama, compile-time code, dynamic code generation, debugging, template language, run-time code, System.Reflection, parameters, auxiliary templates"
created-date: 2023-02-20
modified-date: 2024-08-04
---

# Writing T# templates

Code templates in Metalama are written in a dialect of C#, known as T#. The syntax of T# is fully compatible with C#, but T# is compiled differently.

This chapter includes the following articles:

<table>
    <tr>
        <th>Article</th>
        <th>Description</th>
    </tr>
    <tr>
        <td>
            <xref:template-overview>
        </td>
        <td>
            This article provides an introduction to T#, the template language for Metalama.
        </td>
    </tr>
    <tr>
        <td>
            <xref:template-compile-time>
        </td>
        <td>
            This article outlines the subset of the C# language that can be used as compile-time code and illustrates how to create templates with rich compile-time logic.
        </td>
    </tr>
     <tr>
        <td>
            <xref:dynamic-typing>
        </td>
        <td>
            This article explains the use of `dynamic` typing in templates.
        </td>
    </tr>
    <tr>
        <td>
            <xref:run-time-expressions>
        </td>
        <td>
            This article details different techniques for generating expressions dynamically.
        </td>
    </tr>
    <tr>
        <td>
            <xref:run-time-expressions>
        </td>
        <td>
            This article explains how to generate expressions accessing members once you have their compile-time `IMethod`, `IProperty`, ...
        </td>
    </tr>
    <tr>
        <td>
            <xref:run-time-statements>
        </td>
        <td>
            This article lists techniques for generating statements dynamically.
        </td>
    </tr>
    <tr>
        <td>
            <xref:reflection>
        </td>
        <td>
            This article clarifies how to generate run-time `System.Reflection` objects for compile-time `Metalama.Framework.Code` objects from a template.
        </td>
    </tr>
    <tr>
        <td>
            <xref:template-parameters>
        </td>
        <td>
            This article describes how to pass parameters, including generic parameters, from the `BuildAspect` method to the template.
        </td>
    </tr>
    <tr>
        <td>
            <xref:auxiliary-templates>
        </td>
        <td>
            This article explains how templates can invoke other templates, referred to as auxiliary templates.
        </td>
    </tr>
    <tr>
        <td>
            <xref:debugging-aspects>
        </td>
        <td>
            This article provides guidance on how to debug templates.
        </td>
    </tr>
</table>



