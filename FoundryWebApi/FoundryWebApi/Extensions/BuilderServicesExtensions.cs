using WebApi.Services;

public static class BuilderServicesExtensions
{

    public static IServiceCollection AddAiServices(this IServiceCollection services)
    {
        services.AddScoped<IAzFoundryService, AzFoundryService>();

        return services;
    }

}