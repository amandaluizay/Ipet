using Ipet.Data.Context;
using Microsoft.EntityFrameworkCore;
using Ipet.API.Configuration;
using Ipet.APIConfiguration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();


builder.Services.AddDbContext<MeuDbContext>(options =>
{
    options.UseMySql("server=localhost;initial catalog = ipet;uid=root;pwd=root",
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.0-mysql")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddIdentityConfig(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddApiConfig();

builder.Services.AddSwaggerConfig();

builder.Services.AddLoggingConfig(builder.Configuration);

builder.Services.ResolveDependencies();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure

app.UseApiConfig(app.Environment);

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.UseLoggingConfiguration();

app.Run();