using API.Extensions;
using API;
using Data;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using API.Extensions.Startup;
using Microsoft.AspNetCore.Mvc.Versioning;
using API.Controllers.Attributes;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Options;
using API.Extensions.Startup.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Shared.Interface;
using Shared.Service;
var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
var appSettings = new AppSettings();
configuration.GetSection("AppSettings").Bind(appSettings);

builder.Services.AddControllers(options => { options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())); });

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = V1Attribute.Version;
    options.Conventions.Add(new VersionByNamespaceConvention());
    options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("api-version"));
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSingleton(appSettings);
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VisitContext>(options =>
    options.UseSqlServer(appSettings.ConnectionStrings.Orders));

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IVisitService, VisitService>();
builder.Services.AddTransient<IDatabaseService, DatabaseService>();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }

        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

await app.MigrateDatabasesAsync();

app.Run();
