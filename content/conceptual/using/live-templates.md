---
uid: applying-live-templates
level: 100
summary: "Live templates modify source code within an editor and can be accessed from the refactoring menu. They are a one-time operation and can be added to a project or package reference. "
keywords: "live templates, refactoring menu"
created-date: 2023-02-16
modified-date: 2025-11-30
---

# Using live templates

Aspects, as you may already know, modify the code during compilation while leaving your source code intact. In contrast, live templates transform your source code within your editor. Once you apply a live template to a declaration, you can edit the generated code. Live templates are a one-time operation. Unlike aspects, if the author of the live template modifies the code generation rules after you've applied the template to your source code, your source code won't be modified.

You can access live templates from the refactoring menu, also known as the _lightbulb_ or _screwdriver_ menu.

## Step 1. Add a project or package reference

Similar to aspects, live templates are declared in projects or NuGet packages that you add as references to your project.

The first step involves adding the aspect library to your project using a `<ProjectReference>` or a `<PackageReference>`. This step makes the aspect available in the refactoring menu. If necessary, you can remove the reference after the operation.

> [!NOTE]
> If you only use live templates from this project, consider using the `PrivateAssets="all"` option to prevent the reference from impacting other projects that consume your project.

## Step 2. Apply the live template

1. Position the caret on the name of the declaration to which you want to apply the aspect.
2. Click on the lightbulb or refactoring icon and choose _Apply live template_.
    ![Screenshot](images/LiveTemplate1.png)
3. Select the aspect that you wish to apply.
    ![Screenshot](images/LiveTemplate2.png)

> [!NOTE]
> The author of the live template has the ability to customize the appearance of the live template in the refactoring menu. Consequently, it may be found under a different menu item than _Apply live template_.

> [!div class="see-also"]
>
> **See also**
>
> <xref:using>
> <xref:live-template>
> <xref:quickstart-adding-aspects>

