using Infrastructure.DAL.SeedData;

namespace BookStore.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseOpenAPITools(this WebApplication app)
        {
            // Use Swagger
            //app.UseSwagger();
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            //    options.RoutePrefix = string.Empty;
            //    options.InjectStylesheet("/OpenAPITools/SwaggerUI/custom.css");
            //});

            // Use NSwag
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }

        public static void UseSeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            SeedData.Initialize(scope.ServiceProvider);
        }
    }
}
