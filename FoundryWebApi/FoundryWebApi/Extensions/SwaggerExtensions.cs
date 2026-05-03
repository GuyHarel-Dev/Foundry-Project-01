using Microsoft.OpenApi;

namespace WebApi.Extensions
{
    public static class SwaggerExtensions
    {

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FoundryWebApi",
                    Version = "v1",
                    Description = "API de démonstration IA : génération de texte, résumé et classification.",
                    Contact = new OpenApiContact
                    {
                        Name = "Guy Harel",
                        Email = "guyharel-dev@gmail.com"
                    }
                });
            });

            return services;
        }

        public static WebApplication UseSwaggerDocumentation(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}