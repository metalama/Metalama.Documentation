---
uid: configuration-custom-merge
level: 400
summary: "The document details the process of customizing the change merging process in the Metalama Framework. It outlines how options are merged, the order of merging, disabling inheritance axes, and customizing the merging process."
keywords: "customizing change merging process, Metalama Framework, options inheritance, ApplyChanges method, ApplyChangesContext, IHierarchicalOptionsProvider, SetOptions method, ApplyChangesAxis, disabling inheritance axes, hand-tuning merging process"
created-date: 2024-08-04
modified-date: 2024-08-04
---

# Customizing the change merging process

By default, options are inherited along several axes (<xref:Metalama.Framework.Options.ApplyChangesAxis>). When multiple options apply to the same declaration, they are merged using the <xref:Metalama.Framework.Options.IIncrementalObject.ApplyChanges*> method. This method takes in an <xref:Metalama.Framework.Options.ApplyChangesContext> object, which exposes the <xref:Metalama.Framework.Options.ApplyChangesAxis> over which options are being merged.

> [!WARNING]
> To ensure a consistent user experience across different aspect libraries, aspect authors are advised to customize options inheritance only when there is a compelling reason to do so.

## Merging of options defined for a specific declaration

If multiple sources (such as fabrics, attributes, aspects) set the options for the same declaration, these options are first merged at the level of the node where they have been defined, irrespective of any inheritance. The sources are evaluated in the following order:

1. Options provided by _custom attributes_ implementing <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider>, except aspects. When overriding options provided by competing custom attributes, Metalama uses the axis named <xref:Metalama.Framework.Options.ApplyChangesAxis.SameDeclaration>.
2. Options set by _fabrics_ using <xref:Metalama.Framework.Options.OptionQueryExtensions.SetOptions*?text=amender.Outgoing.SetOptions>. Metalama continues to use the <xref:Metalama.Framework.Options.ApplyChangesAxis.SameDeclaration> axis when applying options originating from <xref:Metalama.Framework.Options.OptionQueryExtensions.SetOptions*>.
3. Options set by _aspects_ using <xref:Metalama.Framework.Options.OptionQueryExtensions.SetOptions*?text=aspectBuilder.Outgoing.SetOptions>. The <xref:Metalama.Framework.Options.ApplyChangesAxis.SameDeclaration> axis is still used.
4. Options provided by _aspects_ implementing <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider>. In this case, the <xref:Metalama.Framework.Options.ApplyChangesAxis.Aspect> axis is used.

## Merging of inherited options

Once all options have been merged at the level of a specific declaration, inheritance rules apply. Options are applied in the following order of priority, with the first items on the list being overridden by the subsequent items:

1. Default options provided by the <xref:Metalama.Framework.Options.IHierarchicalOptions.GetDefaultOptions*> method for the current project.
2. Namespace-level options, from the root to the leaves. When merging namespace-level options with each other and with default options, the axis named <xref:Metalama.Framework.Options.ApplyChangesAxis.ContainingDeclaration> is used.
3. Options of the base type or overridden member. When base options override namespace options, the <xref:Metalama.Framework.Options.ApplyChangesAxis.BaseDeclaration> axis is used. Note that when considering options of the base type or member, fully merged options are considered. This means that namespace-level options of the base type take precedence over the namespace-level options of the current type. In the case of cross-project type inheritance, the default options of the base project take precedence over the default options of the current project.
4. Options of the enclosing declaration, recursively, up to but not including the level of namespaces. When options of the declaring type override options inherited from the base type or member, the <xref:Metalama.Framework.Options.ApplyChangesAxis.ContainingDeclaration> axis is used.
5. Options defined on the target declaration itself. The name of this axis is <xref:Metalama.Framework.Options.ApplyChangesAxis.TargetDeclaration>.

While these details may initially seem complex, they ensure an intuitive use of options.

## Disabling inheritance axes

If you don't want your options to be inherited along one of the above axes, you can annotate your option class with the <xref:Metalama.Framework.Options.HierarchicalOptionsAttribute?text=[HierarchicalOptions]> attribute and set one of these properties to `false`: <xref:Metalama.Framework.Options.HierarchicalOptionsAttribute.InheritedByDerivedTypes>, <xref:Metalama.Framework.Options.HierarchicalOptionsAttribute.InheritedByMembers>, <xref:Metalama.Framework.Options.HierarchicalOptionsAttribute.InheritedByNestedTypes>, or <xref:Metalama.Framework.Options.HierarchicalOptionsAttribute.InheritedByOverridingMembers>.

## Hand-tuning the merging process

If you need to further customize the merging process, you can make your <xref:Metalama.Framework.Options.IIncrementalObject.ApplyChanges*> implementation depend on the <xref:Metalama.Framework.Options.ApplyChangesContext> parameter. This allows you to handle each option property differently according to the context.

Please note that this should be considered an extreme case and we currently do not see a valid use case at the time of writing this documentation.



