using BookStore.API.Extensions;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler(options => options.ExceptionHandlingPath = "/error/dev");

builder
    .AddOpenAPITools()
    .AddCustomRoutes()
    .AddAppInfrastructure()
    .AddCustomControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenAPITools();
    app.UseSeedData();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
