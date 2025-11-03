namespace Vinder.Comanda.Profiles.WebApi.Extensions;

public static class SpecificationsExtension
{
    public static void UseSpecification(this IEndpointRouteBuilder app)
    {
        app.MapScalarApiReference(options =>
        {
            options.DarkMode = false;
            options.HideDarkModeToggle = true;
            options.HideClientButton = true;

            options.WithTitle("Vinder Comanda Profiles API");
            options.WithClassicLayout();
        });
    }
}