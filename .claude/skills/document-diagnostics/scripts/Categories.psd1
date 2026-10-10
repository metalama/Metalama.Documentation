# Categories of the Errors and warnings reference, in table-of-contents order.
#
# Each diagnostic belongs to the first category whose SourceCategories contains the category of its
# DiagnosticDefinition, or whose FilePatterns match its defining file (for definitions without a category).
# Articles store the key in their 'diagnostic-category' front matter. Update-ErrorIndex.ps1 generates one
# page per category with at least one article: content/errors/errors-<key>.md (uid errors-<key>).
@{
    Categories = @(
        @{
            Key              = 'general'
            Title            = 'General errors and warnings'
            Description      = 'Diagnostics about applying aspects, aspect classes and their attributes, eligibility, aspect ordering, fabrics, and the compilation pipeline.'
            SourceCategories = @('Metalama.General', 'Metalama.AttributeDeserializer', 'Metalama.GeneratedCodeAnalyzer', 'Metalama.CodeFixes')
            FilePatterns     = @('*\Metalama.Framework\Diagnostics\*')
        }
        @{
            Key              = 'templates'
            Title            = 'Template errors and warnings'
            Description      = 'Diagnostics about T# templates: compile-time and run-time scopes, unsupported C# constructs, and template parameters.'
            SourceCategories = @('Metalama.Template')
        }
        @{
            Key              = 'advising'
            Title            = 'Advising errors and warnings'
            Description      = 'Diagnostics about advice: overriding and introducing members, implementing interfaces, adding initializers, and conflicts between aspects.'
            SourceCategories = @('Metalama.Advices', 'Metalama.Linker')
        }
        @{
            Key              = 'serialization'
            Title            = 'Serialization errors and warnings'
            Description      = 'Diagnostics about converting compile-time values into run-time code, and about the serialization of aspects and fabrics across projects.'
            SourceCategories = @('Metalama.Serialization')
        }
        @{
            Key              = 'design-time'
            Title            = 'Design-time errors and warnings'
            Description      = 'Diagnostics reported by the IDE integration of Metalama.'
            SourceCategories = @('Metalama.DesignTime')
        }
        @{
            Key              = 'licensing'
            Title            = 'Licensing errors and warnings'
            Description      = 'Diagnostics about the Metalama license and the features it includes.'
            SourceCategories = @('Metalama.Licensing')
            FilePatterns     = @('*\Metalama.Licensing*')
        }
        @{
            Key              = 'architecture'
            Title            = 'Architecture verification errors and warnings'
            Description      = 'Diagnostics reported by Metalama.Extensions.Architecture and Metalama.Extensions.Validation.'
            SourceCategories = @('Metalama.Extensions.Architecture', 'Metalama.Extensions.Validation')
            FilePatterns     = @('*\Metalama.Extensions.Validation*', '*\Metalama.Extensions.Architecture*')
        }
        @{
            Key              = 'dependency-injection'
            Title            = 'Dependency injection errors and warnings'
            Description      = 'Diagnostics reported by Metalama.Extensions.DependencyInjection.'
            SourceCategories = @('Metalama.Extensions.DependencyInjection')
        }
        @{
            Key              = 'caching'
            Title            = 'Caching errors and warnings'
            Description      = 'Diagnostics reported by Metalama.Patterns.Caching.'
            SourceCategories = @('Metalama.Patterns.Caching')
        }
        @{
            Key              = 'contracts'
            Title            = 'Contracts errors and warnings'
            Description      = 'Diagnostics reported by Metalama.Patterns.Contracts.'
            SourceCategories = @('Metalama.Patterns.Contracts')
            FilePatterns     = @('*\Metalama.Patterns.Contracts\*')
        }
        @{
            Key              = 'immutability'
            Title            = 'Immutability errors and warnings'
            Description      = 'Diagnostics reported by Metalama.Patterns.Immutability.'
            SourceCategories = @('Metalama.Patterns.Immutability')
            FilePatterns     = @('*\Metalama.Patterns.Immutability\*')
        }
        @{
            Key              = 'observability'
            Title            = 'Observability errors and warnings'
            Description      = 'Diagnostics reported by Metalama.Patterns.Observability.'
            SourceCategories = @('Metalama.Patterns.Observability')
        }
        @{
            Key              = 'wpf'
            Title            = 'WPF errors and warnings'
            Description      = 'Diagnostics reported by Metalama.Patterns.Wpf.'
            SourceCategories = @('Metalama.Patterns.Wpf')
        }
    )
}
