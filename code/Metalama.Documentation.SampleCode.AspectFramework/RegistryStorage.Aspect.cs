// This is public domain Metalama sample code.

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Microsoft.Win32;
using System;
using System.Linq;

namespace Doc.RegistryStorage;

internal class RegistryStorageAttribute : TypeAspect
{
    public string Key { get; }

    public RegistryStorageAttribute( string key )
    {
        this.Key = "HKEY_CURRENT_USER\\SOFTWARE\\Company\\Product\\" + key;
    }

    public override void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        foreach ( var property in builder.Target.FieldsAndProperties.Where(
                     p => p is { IsImplicitlyDeclared: false, IsAutoPropertyOrField: true } ) )
        {
            builder.With( property ).Override( nameof(this.OverrideProperty) );
        }
    }

    [Template]
    private dynamic? OverrideProperty
    {
        get
        {
            var value = Registry.GetValue( this.Key, meta.Target.FieldOrProperty.Name, null );

            if ( value != null )
            {
                return Convert.ChangeType( value, meta.Target.FieldOrProperty.Type.ToType() );
            }
            else
            {
                return default;
            }
        }

        set
        {
            var stringValue = Convert.ToString( value );
            Registry.SetValue( this.Key, meta.Target.FieldOrProperty.Name, stringValue );
            meta.Proceed();
        }
    }
}