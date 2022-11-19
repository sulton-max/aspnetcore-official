using System.Net;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAD"));
//{
//    options.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
//    options.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseStatusCodePages(async context =>
// {
//     var response = context.HttpContext.Response;
//     if (response.StatusCode == (int)HttpStatusCode.Unauthorized || response.StatusCode = (int)HttpStatusCode.Forbidden)
//         response.Redirect(('/signin-microsoft'));
// });

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();