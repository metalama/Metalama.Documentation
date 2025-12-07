---
uid: quickstart-adding-aspects
level: 100
summary: "This document provides a guide on how to add aspects to source code using custom attributes. It covers the process of adding aspects as custom attributes, adding multiple attributes, and using the refactoring menu."
keywords: "Metalama, getting started"
created-date: 2023-02-16
modified-date: 2025-12-07
---

# Adding aspects to your code

Aspects are custom attributes applied to target declarations. Some aspects target methods, while others target properties or classes.

In this article, you'll learn how to add aspects using custom attributes.

## Adding aspects as custom attributes

Assume you have a method that occasionally fails.

![](images/flaky_method_no_aspect.png)

Currently, CodeLens displays `No aspect`, indicating that no aspect has been applied to this method.

> [!NOTE]
> The CodeLens feature is only available in Visual Studio when Visual Studio Tools for Metalama are installed. See <xref:ide-configuration> for IDE-specific information.

To apply the `Retry` aspect, add it as a standard custom attribute by typing `[Retry]`:

![](images/applying_retry_attribute.png)

CodeLens now displays `1 aspect`. Hover your cursor over that text to reveal the following tooltip:

![](images/retry_aspect_applied.png)

To view the details, click on the text `1 aspect`:

![Retry_Aspect_Code_Lense](images/showing_retry_aspect_code_lense.png)

The details displayed in this example are trivial. However, this feature is useful when you have several aspects on the same method or when aspects are implicitly applied without a custom attribute.

## Adding multiple attributes

Add as many aspects as needed to a target method. In this example, to log each retry attempt, add the `Log` aspect.

![Retry_and_Log_Aspect_Together](images/retry_and_log_aspect_together.png)

CodeLens now shows that two aspects have been applied to the method `FlakyMethod`. Click on the text `2 aspects` to view the details provided by CodeLens:

![Retry_Log_Applied_CodeLense](images/retry_log_code_lense_details.png)

## Adding aspects via the refactoring menu

Instead of manually adding attributes, add them through the refactoring menu. Access this menu by clicking the _lightbulb_ or _screwdriver_ icon or by pressing `Ctrl + .`.

![Context_menu_offers_aspects](images/add_aspect_via_context_menu.png)

The refactoring menu displays three aspects that can be applied to this method. Hover over a menu item to preview your code with the aspect custom attribute.

The refactoring menu recognizes which aspects have already been applied and adjusts its recommendations accordingly. The following screenshot shows that after applying the `Retry` aspect, the refactoring menu only displays the remaining available aspects.

![Successive_application_of_aspects_via_context_menu](images/successive_application_aspects_via_context_menu.png)

> [!NOTE]
> The refactoring menu only displays aspects that are _eligible_ for your code. The aspect author determines eligibility. For example, a caching aspect wouldn't make sense on a method returning `void`, so the aspect author might restrict it to non-void methods only.

> [!div class="see-also"]
>
> **See also**
>
> <xref:using-metalama>
> <xref:fabrics-adding-aspects>
> <xref:getting-aspects>
> <xref:understanding-your-code-with-aspects>
