﻿namespace ApiSourceGenerator;

[Generator]
public class EndpointsGenerator : IIncrementalGenerator
{
    private const string EndpointsNamespace = "GeneralApi.Endpoints";
    private const string EndpointsExtensionsClassName = "EndpointsExtensions";
    private const string MapEndpointsMethodName = "MapEndpoints";
    private const string InterfaceName = "IEndpoint";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(
            (node, _) => node is ClassDeclarationSyntax,
            (syntaxContext, _) => (ClassDeclarationSyntax)syntaxContext.Node)
            .Where(x => x is not null);

        var compilation = context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(compilation, Execute);
    }

    private static void Execute(
        SourceProductionContext context,
        (Compilation Compilation, ImmutableArray<ClassDeclarationSyntax> Classes) tuple)
    {
        var (compilation, classes) = tuple;

        const string prefixCode = $$"""
                                    // <auto-generated />

                                    using {{EndpointsNamespace}};

                                    namespace {{nameof(ApiSourceGenerator)}};

                                    public static class {{EndpointsExtensionsClassName}}
                                    {
                                        public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
                                        {
                                    """;

        var codeBuilder = new StringBuilder(prefixCode);
        codeBuilder.AppendLine();

        List<string> endpointClassesSymbolNames =
        [
            .. from syntax in classes
            let symbol = compilation.GetSemanticModel(syntax.SyntaxTree).GetDeclaredSymbol(syntax) as INamedTypeSymbol
            where symbol is { Name.Length: > 0 }
            where symbol.AllInterfaces.Any(i => i.Name == InterfaceName)
            orderby symbol.Name, StringComparer.OrdinalIgnoreCase
            select symbol.Name
        ];

        foreach (var symbolName in endpointClassesSymbolNames)
        {
            codeBuilder.AppendLine($"        new {symbolName}().{MapEndpointsMethodName}(app);");
        }

        const string suffixCode = """
                                          return app;
                                      }
                                  }
                                  """;

        codeBuilder.AppendLine(suffixCode);

        context.AddSource($"{EndpointsExtensionsClassName}.g.cs", codeBuilder.ToString());
    }
}
