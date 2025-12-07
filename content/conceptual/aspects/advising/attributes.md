---
uid: adding-attributes
summary: "The document provides a guide on how to add or remove custom attributes to or from any declaration using the Metalama Framework. It includes examples and methods for existing and introduced declarations."
keywords: "custom attributes, Metalama Framework, add attributes, remove attributes, existing declaration, introduced declaration, introduce attribute, attribute construction, AdviserExtensions, IDeclarationBuilder"
created-date: 2023-07-11
modified-date: 2025-11-30
---

# Adding or removing custom attributes

An aspect can add or remove custom attributes to or from any declaration. There are several ways to accomplish this, depending on whether the declaration exists in the code model or is being added by the aspect.

## Adding attributes to an existing declaration

To add a custom attribute to a declaration that exists before the aspect is applied, use the <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceAttribute*> method. This method requires an argument of type <xref:Metalama.Framework.Code.IAttributeData>. This interface is implemented by the <xref:Metalama.Framework.Code.IAttribute> interface, allowing you to introduce any custom attribute that you find in the code model via the <xref:Metalama.Framework.Code.IDeclaration.Attributes?text=IDeclaration.Attributes> property. To create a new custom attribute, use the <xref:Metalama.Framework.Code.DeclarationBuilders.AttributeConstruction.Create*?text=AttributeConstruction.Create> method. This method requires the attribute type or constructor along with two optional sets of arguments: _constructor arguments_ are the arguments of the constructor, and _named arguments_ are the values assigned to fields and properties.

### Example: Adding EditorBrowsableAttribute to fields

The following aspect adds <xref:System.ComponentModel.EditorBrowsableAttribute> to all fields whose name starts with a double underscore.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AddEditorBrowsableAttribute.cs name="Add attributes to existing fields"]

## Removing attributes from an existing declaration

To remove all custom attributes of a given type from a declaration, use the <xref:Metalama.Framework.Aspects.AdviserExtensions.RemoveAttributes*> method.

You cannot edit a custom attribute; instead, you must remove previous instances and add new ones.

## Adding attributes to an introduced declaration, declaratively

When your aspect introduces a new declaration in a declarative way, i.e., using the <xref:Metalama.Framework.Aspects.IntroduceAttribute?text=[Introduce]> custom attribute, you can add any custom attribute to the new member by adding the attribute to the template.

### Example: Declaratively introducing a field with EditorBrowsableAttribute

The next example demonstrates an aspect that introduces a field hidden from the editor by <xref:System.ComponentModel.EditorBrowsableAttribute>. The custom attribute is copied from the template to the target declaration.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AddEditorBrowsableAttribute_Introduced_Declarative.cs name="Add attributes to introduced field"]

## Adding attributes to an introduced declaration, programmatically

The second, more advanced method to introduce declarations into a type is to call one of the `Introduce*` methods of the <xref:Metalama.Framework.Aspects.AdviserExtensions> class. This technique is described in <xref:introducing-members>. These methods accept an optional delegate that can configure the declaration being introduced. This delegate receives an <xref:Metalama.Framework.Code.DeclarationBuilders.IDeclarationBuilder>, and you can use the <xref:Metalama.Framework.Code.DeclarationBuilders.IDeclarationBuilder.AddAttribute*> and <xref:Metalama.Framework.Code.DeclarationBuilders.IDeclarationBuilder.RemoveAttributes*> methods as described above.

### Example: Programmatically introducing a field with EditorBrowsableAttribute

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AddEditorBrowsableAttribute_Introduced_Programmatic.cs name="Add attributes to introduced field"]

> [!div class="see-also"]
> <xref:introducing-members>
> <xref:Metalama.Framework.Aspects.AdviserExtensions.IntroduceAttribute*>
> <xref:Metalama.Framework.Code.DeclarationBuilders.AttributeConstruction>
