---
uid: install-vsx
level: 100
summary: "This document provides step-by-step instructions on how to install the Metalama Tools extension for Visual Studio 2022, and introduces the Metalama Command-Line Tool."
keywords: "Visual Studio Tools, Metalama Tools extension, installation,CodeLens additions, Aspect Explorer, syntax highlighting, development experience, extension download."
created-date: 2023-03-02
modified-date: 2024-08-04
---

# Installing Visual Studio Tools for Metalama

> [!INFO]
> Visual Studio Tools for Metalama require a Metalama Community or Metalama Professional license.

The [Visual Studio Tools for Metalama](https://www.postsharp.net/links/download-unified-vsx) is an extension that enhances your development experience by providing features such as:

* CodeLens additions for quickly viewing the impact of aspects on your code,
* Aspect Explorer for displaying which aspects are available in the current solution and which code is affected,
* Diffing functionality to compare your original source code against the transformed code,
* Syntax highlighting for aspect code.

While this extension is optional, it is highly recommended for a more comprehensive understanding of your aspect-oriented code.

## Downloading the extension

The simplest way to install the extension is to install it from [Visual Studio Marketplace](https://www.postsharp.net/links/download-unified-vsx) and launch the downloaded file.


## Installing from Visual Studio

Alternatively, you can use the following procedure from Visual Studio.

> [!WARNING]
> The following screenshots are outdated and need to be updated due to changes in the logo and the name of the extension.

1. Navigate to `Extensions` > `Manage Extensions`.

    ![step1](../../images/ext_manage_1.png)

2. This action will open the following dialog:

    ![step2](../../images/ext_manage_2.png)

3. In the search box to the right of the prompt, enter "Metalama + PostSharp".

    ![step3](../../images/ext_manage_3.png)

4. Click the `Download` button to initiate the download process.

    ![step4](../../images/ext_manage_4.png)

5. Once the extension is downloaded, it will be ready for installation as soon as all instances of Visual Studio are closed. This requirement is highlighted at the bottom of the screen.

    ![step5](../../images/ext_manage_5.png)

6. Provide consent for the installation of the extension.

    The installation process will commence automatically when Visual Studio is closed.

    ![wizard_init](../../images/ext_manage_6.png)

    The installation wizard operates independently and will request your consent during the following stage:

    ![wizard_asking_consent](../../images/ext_manage_consent.png)

7. Click "Modify" to finalize the installation.

    To proceed with the installation, click the `Modify` button. The wizard will then continue with the installation process.

    ![metalama_install_progress](../../images/metalama_install_progress.png)

    Upon successful installation, the wizard will display the following screen:

    ![metalama_install_done](../../images/metalama_install_done.png)

To verify the successful installation of the extension, navigate to the Extensions Manager. If the installation was successful, a green tick mark will be displayed on the top right corner of the extension icon.

![metalama_already_installed](../../images/metalama_already_installed.png)



