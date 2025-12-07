---
uid: templates
level: 200
summary: "This document provides a guide on writing T# templates in Metalama, covering topics like template language introduction, compile-time code, dynamic code generation, and debugging."
keywords: "T# templates, Metalama, compile-time code, dynamic code generation, debugging, template language, run-time code, System.Reflection, parameters, auxiliary templates"
created-date: 2023-02-20
modified-date: 2025-11-30
---

# Writing T# templates

Code templates are the foundation of how aspects generate and transform code in Metalama. Templates are written in T#, a dialect of C# that combines compile-time logic with run-time code generation. While T# syntax is fully compatible with C#, the compilation process is different: T# code executes at compile-time to generate the C# code that will run in your application.

Templates let you write code that analyzes your codebase at compile-time and generates new code or modifies existing code based on that analysis. This powerful mechanism enables you to automate repetitive coding patterns, enforce architectural rules, and implement cross-cutting concerns.

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
            <xref:invokers>
        </td>
        <td>
            This article explains how to generate expressions that access members once you have their compile-time `IMethod`, `IProperty`, and similar objects.
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
            This article provides guidance on debugging templates.
        </td>
    </tr>
</table>

## Getting started

To begin working with templates, start with <xref:template-overview> to understand the fundamentals of T# and how compile-time and run-time code work together. Once you're comfortable with the basics, explore the other articles in this chapter to learn about specific techniques and advanced features.

> [!div class="see-also"]
> <xref:aspects>
> <xref:simple-aspects>
> <xref:Metalama.Framework.Aspects.meta>
> <xref:Metalama.Framework.Aspects.TemplateAttribute>
