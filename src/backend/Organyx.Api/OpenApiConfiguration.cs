using Microsoft.AspNetCore.OpenApi;
using Scalar.AspNetCore;

namespace Organyx.Api;

public sealed record OpenApiDocumentInfo(string Name, string Title, string Description);

public static class OpenApiDocuments
{
    public static IReadOnlyList<OpenApiDocumentInfo> All { get; } =
    [
        new("development", "Development API", "Development endpoints used for building Organyx."),
        new("application", "Application API", "API used by the Organyx frontend."),
    ];
}

public static class OpenApiConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddOrganyxOpenApi()
        {
            foreach (var doc in OpenApiDocuments.All)
            {
                services.AddOpenApi(doc.Name, options =>
                {
                    options.AddDocumentTransformer((document, _, _) =>
                    {
                        document.Info.Title = doc.Title;
                        document.Info.Description = doc.Description;
                        return Task.CompletedTask;
                    });
                });
            }

            return services;
        }
    }

    extension(ScalarOptions options)
    {
        public ScalarOptions AddOrganyxDocuments()
        {
            foreach (var doc in OpenApiDocuments.All)
                options.AddDocument(doc.Name, doc.Title);

            return options;
        }
    }
}
