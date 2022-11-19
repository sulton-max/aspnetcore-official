using BookStore.API.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace BookStore.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddOpenAPITools(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();

            // Add SWagger
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Version = "v1",
            //        Title = "BookStore API",
            //        Description = "Sample Book Store API for CRUD operations",
            //        TermsOfService = new Uri("https://example.com"),
            //        Contact = new OpenApiContact
            //        {
            //            Name = "Example contact",
            //            Url = new Uri("https://example.com")
            //        },
            //        License = new OpenApiLicense
            //        {
            //            Name = "Example license",
            //            Url = new Uri("https://example.com")
            //        }
            //    });

            //    var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));
            //});

            // Add NSwag
            builder.Services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Title = "BookStore.API Doc";
                    document.Info.Version = "v1";
                    document.Info.Description = "Book Storage API Documentation for test";
                    document.Info.TermsOfService = "Use under policies v3";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "MaxDev",
                        Email = "example@gmail.com",
                        Url = "https://example.com"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com"
                    };
                };
            });

            return builder;
        }

        public static WebApplicationBuilder AddCustomControllers(this WebApplicationBuilder builder)
        {
            // Add Newtonsoft.JSON input/output formatter
            // Replaces System.Text.Json input/output formatters
            builder.Services
                .AddControllers(options =>
                {
                    // Respect browser accept header
                    options.RespectBrowserAcceptHeader = true;

                    // Add custom formatters
                    options.InputFormatters.Insert(0, new VcardInputFormatter());
                    options.OutputFormatters.Insert(0, new ExcelOutputFormatter());
                    options.OutputFormatters.Insert(0, new WordOutputFormatter());
                    options.OutputFormatters.Insert(0, new VcardOutputFormatter());
                })
                .AddXmlSerializerFormatters()
                .AddXmlDataContractSerializerFormatters()
                .AddNewtonsoftJson();

            // Add only JSON PATCH input foratter
            //services.
            //    AddControllers(options =>
            //    {
            //        options.InputFormatters.Insert(0, FormatterExtensions.GetJsonPatchInputFormatter());
            //    });

            return builder;
        }

        public static WebApplicationBuilder AddCustomRoutes(this WebApplicationBuilder builder)
        {
            builder.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            return builder;
        }
    }
}
