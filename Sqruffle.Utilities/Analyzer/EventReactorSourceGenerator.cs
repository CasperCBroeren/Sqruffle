using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Text;

[Generator]
public class EventReactorSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {         
        // Define a syntax provider to find classes implementing IEventReactor<T>
        var allClassesImplementing = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (node, _) => IsClassWithBaseList(node),
                transform: static (ctx, _) => GetClassAndGenericType(ctx))
            .Where(static result => result is not null);

        // Collect unique classes
        var distinctClasses = allClassesImplementing
            .Collect()
            .Select(static (items, _) => items.Distinct());
        var generated = new List<string>();
        // Register the generation logic
        context.RegisterSourceOutput(distinctClasses, (context, classes) =>
        {
            foreach (var x in classes)
            {
                if (x.HasValue && x.Value.GenericTypeName != null)
                {
                    var genericTypeName = x.Value.GenericTypeName;
                    var generatedClassName = $"{genericTypeName}ConsumerListener";
                    var source = GenerateConsumerListenerClass(genericTypeName, generatedClassName);
                    if (!generated.Contains(generatedClassName))
                    {
                        context.AddSource($"{generatedClassName}.g.cs", SourceText.From(source, Encoding.UTF8));
                        generated.Add(generatedClassName);
                    }
                }
            }
        });
    }

    private static bool IsClassWithBaseList(SyntaxNode node)
    {
        return node is ClassDeclarationSyntax classDecl && classDecl.BaseList is not null;
    }

    private static (string? ClassName, string? GenericTypeName)? GetClassAndGenericType(GeneratorSyntaxContext context)
    {
        var classDeclaration = (ClassDeclarationSyntax)context.Node;

        foreach (var baseType in classDeclaration.BaseList?.Types ?? Enumerable.Empty<BaseTypeSyntax>())
        {
            var typeSymbol = context.SemanticModel.GetSymbolInfo(baseType.Type).Symbol as INamedTypeSymbol;
            if (typeSymbol is null || typeSymbol.Name != "IEventReactor" || !typeSymbol.IsGenericType)
                continue;

            var genericTypeName = typeSymbol.TypeArguments[0].Name;
            var className = classDeclaration.Identifier.Text;
            return (className, genericTypeName);
        }

        return null;
    } 

    private static string GenerateConsumerListenerClass(string eventName, string className)
    {
        return $@" 
using Sqruffle.Utilities.Analyzer;
using Sqruffle.Domain.Feature;
using Sqruffle.Domain.Products.Events;
using Sqruffle.Domain.General.Events;

public class {className} : AConsumerEventListener<{eventName}>
{{
    public {className}(IFeatureReactionFinder featureReactionFinder) : base(featureReactionFinder)
    {{
    }}
}}
";
    }
}
