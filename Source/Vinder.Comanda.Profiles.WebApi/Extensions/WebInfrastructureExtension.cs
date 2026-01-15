namespace Vinder.Comanda.Profiles.WebApi.Extensions;

[ExcludeFromCodeCoverage(Justification = "contains only web infrastructure configuration with no business logic.")]
public static class WebInfrastructureExtension
{
    public static void AddWebComposition(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddCorsConfiguration();
        services.AddIdentityServer();
        services.AddFluentValidationAutoValidation(options =>
        {
            options.DisableDataAnnotationsValidation = true;
        });

        var provider = services.BuildServiceProvider();
        var settings = provider.GetRequiredService<ISettings>();

        services.AddOpenApi(options =>
        {
            options.AddScalarTransformers();
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Components ??= new Microsoft.OpenApi.Models.OpenApiComponents();
                document.Components.SecuritySchemes[SecuritySchemes.OAuth2] =
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2,
                        Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows
                        {
                            ClientCredentials = new Microsoft.OpenApi.Models.OpenApiOAuthFlow
                            {
                                TokenUrl = new Uri(settings.Federation.BaseUrl + "/api/v1/openid/connect/token")
                            }
                        }
                    };

                return Task.CompletedTask;
            });
        });
    }
}