---
uid: contract-types
---

# List of contract attributes

Below is a list of available contract attributes for your selection:

## Nullability contracts

### [NotNull]

The <xref:Metalama.Patterns.Contracts.NotNullAttribute> contract verifies that the assigned value is not `null`.

#### Example: [NotNull]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/NotNullContract.cs]

### [Required]

Similar to the <xref:Metalama.Patterns.Contracts.NotNullAttribute> contract, the <xref:Metalama.Patterns.Contracts.RequiredAttribute> contract verifies that the value is not `null`. Additionally, it requires the string to be non-empty.

#### Example: [Required]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/RequiredContract.cs]

> [!WARNING]
> All contracts listed below accept `null` values without validating them.

## String contracts

### [NotEmpty]

The <xref:Metalama.Patterns.Contracts.NotEmptyAttribute> contract requires the string to be non-empty. Please note that this contract does not validate the string against being null. If you want to prohibit both null and empty strings, use the <xref:Metalama.Patterns.Contracts.RequiredAttribute> constraint.

#### Example: [NotEmpty] vs [NotNull] vs [Required]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/NotEmptyContract.cs]

### [CreditCard]

The <xref:Metalama.Patterns.Contracts.CreditCardAttribute> contract validates that the string is a valid credit card number.

It utilizes the delegate exposed by the <xref:Metalama.Patterns.Contracts.ContractHelpers.IsValidCreditCardNumber?text=ContractHelpers.IsValidCreditCardNumber> property. You can replace this delegate with your own implementation.

#### Example: [CreditCard]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/CreditCardContract.cs]

### [Email], [Phone], and [Url]

The <xref:Metalama.Patterns.Contracts.PhoneAttribute>, <xref:Metalama.Patterns.Contracts.EmailAttribute> and <xref:Metalama.Patterns.Contracts.UrlAttribute> contracts can be used to validate strings against well-known regular expressions.

These regular expressions can be customized by setting the <xref:Metalama.Patterns.Contracts.ContractHelpers.PhoneRegex>, <xref:Metalama.Patterns.Contracts.ContractHelpers.EmailRegex>, <xref:Metalama.Patterns.Contracts.ContractHelpers.UrlRegex> properties of the <xref:Metalama.Patterns.Contracts.ContractHelpers> class.

#### Example: [Email], [Phone], and [Url]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/WellKnownRegexContracts.cs]

### Custom regular expressions

The <xref:Metalama.Patterns.Contracts.RegularExpressionAttribute> contract validates a string against a custom regular expression. If the same regular expression is to be used multiple times, it may be preferable to create a derived class instead of using the <xref:Metalama.Patterns.Contracts.RegularExpressionAttribute> class directly.

#### Example: custom regular expression

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/CustomRegexContract.cs]

### String length

The <xref:Metalama.Patterns.Contracts.StringLengthAttribute> contract validates that the length of a string falls within a specified range.

#### Example: [StringLength]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/StringLengthContract.cs]

## Enum contracts

The <xref:Metalama.Patterns.Contracts.EnumDataTypeAttribute> contract can validate values of type `string`, `object`, or of any integer type. It throws an exception if the value is not valid for the given `enum` type.

### Example: [EnumDataType]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/EnumDataTypeContract.cs]

## Numeric contracts

The following contracts can be used to verify that a value falls within a specified range:

| Attribute                                                 | Description |
|-----------------------------------------------------------|-------------|
| <xref:Metalama.Patterns.Contracts.LessThanAttribute>     | Verifies that the value is less than or equal to the specified maximum.
| <xref:Metalama.Patterns.Contracts.GreaterThanAttribute>  |  Verifies that the value is greater than or equal to the specified minimum.
| <xref:Metalama.Patterns.Contracts.NegativeAttribute>     | Verifies that the value is less than or equal to zero.
| <xref:Metalama.Patterns.Contracts.PositiveAttribute>     | Verifies that the value is greater than or equal to zero.
| <xref:Metalama.Patterns.Contracts.RangeAttribute>     | Verifies that the value is greater than or equal to a specified minimum and less than or equal to a specified maximum.
| <xref:Metalama.Patterns.Contracts.StrictlyLessThanAttribute>     | Verifies that the value is strictly less than the specified maximum.
| <xref:Metalama.Patterns.Contracts.StrictlyGreaterThanAttribute>  |  Verifies that the value is strictly greater than the specified minimum.
| <xref:Metalama.Patterns.Contracts.StrictlyNegativeAttribute>     | Verifies that the value is strictly less than zero.
| <xref:Metalama.Patterns.Contracts.StrictlyPositiveAttribute>     | Verifies that the value is strictly greater than zero.
| <xref:Metalama.Patterns.Contracts.StrictRangeAttribute>     | Verifies that the value is strictly greater than a specified minimum and strictly less than a specified maximum.

### Example: numeric contracts

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/NumericContracts.cs]

## Collections contracts

The <xref:Metalama.Patterns.Contracts.NotEmptyAttribute> contract can be used on any collection, including arrays or immutable arrays. It requires the collection or the array to contain at least one element.

Note that this contract does not validate the collection object against being null (or, in the case of immutable arrays, default ones). If you want to prohibit both null and empty collections, consider adding the <xref:Metalama.Patterns.Contracts.NotNullAttribute> constraint.

### Example: [NotEmpty] on collection

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/NotEmptyCollectionContract.cs]
