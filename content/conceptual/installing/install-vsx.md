---
uid: install-vsx
level: 100
summary: "This document provides step-by-step instructions on how to install the Metalama Tools extension for Visual Studio 2022, and introduces the Metalama Command-Line Tool."
keywords: "Visual Studio extension, CodeLens, Aspect Explorer, syntax highlighting, diffing, installation"
created-date: 2023-03-02
modified-date: 2025-11-30
---

# Installing Visual Studio Tools for Metalama

> [!NOTE]
> Visual Studio Tools for Metalama require a Metalama Community or Metalama Professional license.

The Visual Studio Tools for Metalama are an extension that enhances your development experience by providing features such as:

* CodeLens additions to quickly view the impact of aspects on your code
* Aspect Explorer for displaying which aspects are available in the current solution and which code is affected
* Diffing functionality to compare your original source code against the transformed code
* Syntax highlighting for aspect code

While this extension is optional, it is highly recommended for a more comprehensive understanding of your aspect-oriented code.

## Downloading the extension

The simplest way to install the extension is from [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=PostSharpTechnologies.PostSharp). Download and launch the file.

## Installing from Visual Studio

Alternatively, you can use the following procedure from Visual Studio.

> [!WARNING]
> The following screenshots are outdated and need to be updated due to changes in the logo and the name of the extension.

1. Navigate to `Extensions` > `Manage Extensions`.

    ![step1](../../images/ext_manage_1.png)

2. This opens the following dialog:

    ![step2](../../images/ext_manage_2.png)

3. In the search box, enter "Metalama + PostSharp".

    ![step3](../../images/ext_manage_3.png)

4. Click the `Download` button to download the extension.

    ![step4](../../images/ext_manage_4.png)

5. After the extension downloads, it will be ready for installation when all Visual Studio instances are closed. This requirement appears at the bottom of the screen.

    ![step5](../../images/ext_manage_5.png)

6. Provide consent for the installation.

    Installation starts automatically when Visual Studio closes.

    ![wizard_init](../../images/ext_manage_6.png)

    The installation wizard runs independently and requests your consent:

    ![wizard_asking_consent](../../images/ext_manage_consent.png)

7. Click the `Modify` button to complete the installation.

    ![metalama_install_progress](../../images/metalama_install_progress.png)

    When installation is complete, the wizard displays the following screen:

    ![metalama_install_done](../../images/metalama_install_done.png)

To verify the extension installed successfully, navigate to the Extensions Manager. A green tick mark appears on the top-right corner of the extension icon when installation is successful.

![metalama_already_installed](../../images/metalama_already_installed.png)

> [!div class="see-also"]
> <xref:installing>
> <xref:register-license>
> <xref:understanding-your-code-with-aspects>
