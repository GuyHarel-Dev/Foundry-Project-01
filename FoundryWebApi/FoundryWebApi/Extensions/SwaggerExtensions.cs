using Microsoft.OpenApi;

namespace ProjectExtensions
{
    public static class PrivateSwaggerExtensions
    {

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FP01 AI API",
                    Version = "v1",
                    Description = "API de démonstration IA : génération de texte, résumé et classification.",
                    Contact = new OpenApiContact
                    {
                        Name = "Ton Nom",
                        Email = "ton@email.com"
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