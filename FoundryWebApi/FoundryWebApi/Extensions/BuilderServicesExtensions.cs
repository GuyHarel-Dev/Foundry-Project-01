using Azure.AI.Projects;
using Azure.Identity;
using WebApi.Services;

public static class BuilderServicesExtensions
{

    public static IServiceCollection AddAzFoundryService(this IServiceCollection services)
    {
        services.AddScoped<IAzFoundryChatService, AzFoundryChatService>();

        return services;
    }

    public static IServiceCollection AddAzFoundryClient (this IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();

            var endpoint = config["Foundry-Project-01:Endpoint"]
                ?? throw new InvalidOperationException("Endpoint not configured");

            return new AIProjectClient(
                endpoint: new Uri(endpoint),
                tokenProvider: new DefaultAzureCredential());
        });


        return services;
    }

}