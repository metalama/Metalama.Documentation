---
uid: decoupling-aspects-from-attributes
created-date: 2024-09-05
modified-date: 2024-09-05
---

# Decoupling Aspects From Attributes

When reading other articles in this documentation, you may have come under the impression that an aspect is necessarily a custom attribute. This is not the case. Aspects and attributes are different concepts. You can build aspects that do not derive from the <xref:System.Attribute> class.

The reason why most aspects are derived from the <xref:System.Attribute> class is convenience and simplicity. Indeed, as you can see from their [source code](https://github.com/postsharp/Metalama.Framework/tree/HEAD/Metalama.Framework/Aspects), classes like <xref:Metalama.Framework.Aspects.ConstructorAspect>, <xref:Metalama.Framework.Aspects.EventAspect>, <xref:Metalama.Framework.Aspects.FieldAspect>, <xref:Metalama.Framework.Aspects.FieldOrPropertyAspect>, <xref:Metalama.Framework.Aspects.MethodAspect>, <xref:Metalama.Framework.Aspects.ParameterAspect>, <xref:Metalama.Framework.Aspects.PropertyAspect>, <xref:Metalama.Framework.Aspects.TypeAspect>, or <xref:Metalama.Framework.Aspects.TypeParameterAspect> are only API sugar. They are all implementations of the <xref:Metalama.Framework.Aspects.IAspect`1> interface that derive from <xref:System.Attribute>.

If you want to build an aspect that must not be represented as an attribute, you can implement a class that implements <xref:Metalama.Framework.Aspects.IAspect`1> but not <xref:System.Attribute>. You can then add this aspect using a fabric (see <xref:fabrics-adding-aspects>) or a child aspect (see <xref:child-aspects>).

However, what if you still want the aspect to be added using a custom attribute, but you don't want the custom attribute to implement the <xref:Metalama.Framework.Aspects.IAspect`1> interface? This is what we will see in this article.

## Why would you want to decouple aspects from attributes?

Decoupling an aspect from its attribute means that we are decoupling the aspect _implementation_ from its "interface" or _contract_. This aspect contract is the custom attribute. The implementation is any class implementing the <xref:Metalama.Framework.Aspects.IAspect`1> interface.

This has several benefits:

* The custom attribute can reside in a package for which you don't have the source code (for instance, those of the <xref:System.ComponentModel.DataAnnotations> namespace) or where you cannot or don't want to add a reference to Metalama.
* You can prepare your code for an easier departure from Metalama, either by switching to another code generation or validation technology or by using our `metalama divorce` feature. See <xref:divorcing> for details.
* You can have different implementations of the aspect in different projects or namespaces.

## Decoupling when the contract project _can_ have a reference to Metalama

We will first look at the situation where the project that contains the custom attribute can reference the `Metalama.Framework` project. This approach is simpler because the custom attribute can be constructed into a compile-time object.

### Step 1. Create the custom attribute class

1. Make sure the project has a reference to the `Metalama.Framework` package.
2. Create the class that will become the aspect contract. Derive it from <xref:System.Attribute> and include its constructor parameters and properties.
3. Add two custom attributes to this class:
    * <xref:System.AttributeUsageAttribute?text=[AttributeUsage]>
    * <xref:Metalama.Framework.Aspects.RunTimeOrCompileTimeAttribute?text=[RunTimeOrCompileTime]>
4. Add the <xref:Metalama.Framework.Serialization.ICompileTimeSerializable> interface to the type. This is an empty interface, but it will instruct Metalama to generate a serializer for your attribute. This step is only useful if the aspect is expected to have cross-project effects, for instance aspect inheritance or adding reference validation.

### Step 2. Create the aspect implementation class

This step can be performed in a separate project, which must also have a reference to the `Metalama.Framework` package.

1. Create a class that implements the <xref:Metalama.Framework.Aspects.IAspect`1> interface where `T` is any kind of declaration to which the attribute can be added. For instance, if your attribute can be applied to methods and properties, implement <xref:Metalama.Framework.Aspects.IAspect`1> for both <xref:Metalama.Framework.Code.IMethod> and <xref:Metalama.Framework.Code.IProperty>.

2. Add a constructor that accepts the attribute object and stores it in an instance field.
3. Implement your aspect as usual. The only difference is that the attribute object is not the aspect object itself but is available on the instance field.

### Step 3. Bind the custom attribute to the aspect using a fabric

The last step is to add an aspect for each instance of the custom attribute. This is typically done in a fabric.

1. Create a <xref:Metalama.Framework.Fabrics.ProjectFabric> (or use an existing one).
2. Use the <xref:Metalama.Framework.Fabrics.QueryExtensions.SelectDeclarationsWithAttribute*> method to find all declarations that have the attribute created in Step 1.
3. For declaration types supported by your aspect (i.e., all values of `T` in <xref:Metalama.Framework.Aspects.IAspect`1>), use <xref:Metalama.Framework.Fabrics.IQuery`1.OfType*> to select these declarations.
4. Call the <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*> method. Supply a lambda that instantiates the aspect. To get the attribute instance, use the <xref:Metalama.Framework.Code.IDeclaration.Attributes?text=IDeclaration.Attributes> collection and then the <xref:Metalama.Framework.Code.Collections.IAttributeCollection.GetConstructedAttributesOfType*> method. Pass the instance to the aspect constructor.

### Example: decoupled logging aspect _with_ reference to Metalama.Framework

In this example, we show a traditional logging aspect whose API is a custom attribute that does not implement the <xref:Metalama.Framework.Aspects.IAspect`1>. However, the attribute uses the Metalama-specific <xref:Metalama.Framework.Aspects.RunTimeOrCompileTimeAttribute?text=[RunTimeOrCompileTime]> attribute, which simplifies the implementation of the aspect.

The attribute can be applied to both methods and properties. It has a `Category` property, which the aspect must include in the logged string.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Decoupled.cs name="Decoupled without reference"]

## Decoupling when the contract project _cannot_ have a reference to Metalama

If you can't or don't want to add a reference to `Metalama.Framework` in the contract project, the process is a bit more difficult. The reason is that the custom attribute is a _run-time only_ class, which means that it _cannot_ be constructed at build time. However, the aspect constructor and its `BuildAspect` method all run at _build time_. That means that you cannot pass the attribute itself to the aspect.

Instead, your aspect must have a parameter of type `IRef<IAttribute>`. The <xref:Metalama.Framework.Code.IAttribute> interface plays a similar role as the <xref:System.Reflection.CustomAttributeData?text=System.Reflection.CustomAttributeData> class. It exposes attribute data as the <xref:Metalama.Framework.Code.IAttributeData.ConstructorArguments> and <xref:Metalama.Framework.Code.IAttributeData.NamedArguments> properties. However, the <xref:Metalama.Framework.Code.IAttribute> interface is bound to a specific compilation revision. Aspects should generally not store compilation-specific objects but _references_. This is why we will store an <xref:Metalama.Framework.Code.IRef`1>. References, unlike declarations, are serializable and can therefore be used in cross-project scenarios such as aspect inheritance or cross-project validation.

### Step 1. Create the custom attribute class

This step is very similar to the first step of the first approach but we will not have anything Metalama-related.

1. Create the class that will become the aspect contract. Derive it from <xref:System.Attribute> and include its constructor parameters and properties.
2. Add the <xref:System.AttributeUsageAttribute?text=[AttributeUsage]> custom attribute to this class.

### Step 2. Create the aspect implementation class

We now create the aspect itself. The only difference compared to the first approach is that our constructor parameter must be of type `IRef<IAttribute>`.

1. Create a class that implements the <xref:Metalama.Framework.Aspects.IAspect`1> interface where `T` is any kind of declaration to which the attribute can be added. For instance, if your attribute can be applied to methods and properties, implement <xref:Metalama.Framework.Aspects.IAspect`1> for both <xref:Metalama.Framework.Code.IMethod> and <xref:Metalama.Framework.Code.IProperty>.

2. Add a constructor with a parameter of type `IRef<IAttribute>` and store its value in an instance field of the same type.
3. Implement your aspect as usual. In any aspect method, to get the <xref:Metalama.Framework.Code.IAttribute>, use the <xref:Metalama.Framework.Code.RefExtensions.GetTarget*?text=IRef.GetTarget()> method. Then use the <xref:Metalama.Framework.Code.IAttributeData.ConstructorArguments> and <xref:Metalama.Framework.Code.IAttributeData.NamedArguments> properties to get access to attribute data.

### Step 3. Bind the custom attribute to the aspect using a fabric

The last step is again to add an aspect for each instance of the custom attribute. The main difference is how we pass the attribute to the aspect.

1. Create a <xref:Metalama.Framework.Fabrics.ProjectFabric> (or use an existing one).
2. Use the <xref:Metalama.Framework.Fabrics.QueryExtensions.SelectDeclarationsWithAttribute*> method to find all declarations that have the attribute created in Step 1.
3. For declaration types supported by your aspect (i.e., all values of `T` in <xref:Metalama.Framework.Aspects.IAspect`1>), use <xref:Metalama.Framework.Fabrics.IQuery`1.OfType*> to select these declarations.
4. Call the <xref:Metalama.Framework.Aspects.AspectQueryExtensions.AddAspect*> method. Supply a lambda that instantiates the aspect. To get the attribute data, use the <xref:Metalama.Framework.Code.IDeclaration.Attributes?text=IDeclaration.Attributes> collection and then the <xref:Metalama.Framework.Code.Collections.IAttributeCollection.OfAttributeType*> method. Use the <xref:Metalama.Framework.Code.IAttribute.ToRef*?text=IAttribute.ToRef()> method to get the reference, and pass it to the aspect constructor.

### Example: decoupled logging aspect _without_ any references to Metalama.Framework

We revise the previous example and remove any references to `Metalama.Framework` from the contract attribute. Therefore, the attribute must be handled as an `IRef<IAttribute>`. As you can see in the aspect implementation, the logic to retrieve the `Category` is more complex.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Decoupled_Ref.cs name="Decoupled with reference"]

