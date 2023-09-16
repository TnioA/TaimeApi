using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Taime.Application.Data.MySql;
using Taime.Application.Settings;
using Taime.Application.Utils.Extensions;
using static Taime.Application.Helpers.EnvLoaderHelper;

// App Configuration
var builder = WebApplication.CreateBuilder(args);

// Load Envs
Load();

//Set Data Services
builder.Services.AddApiCallRepositories();
builder.Services.AddMySql(GetValueFromEnv<string>("KEY_MYSQL_CONN_STR"));

// Set Auto Inject Services
builder.Services.AddBaseServices();

// Set Controllers / configuring routes
builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

// Set Settings
var settings = new AppSettings();
builder.Services.AddSingleton(settings);

// Set Authentication
var key = Encoding.ASCII.GetBytes(settings.JWTAuthorizationToken);
builder.Services.AddAuthentication(configureOptions =>
{
    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(configureOptions =>
{
    configureOptions.RequireHttpsMetadata = true;
    configureOptions.SaveToken = true;
    configureOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Set Api Versioning
builder.Services.AddApiVersioning(setup =>
{
    setup.ReportApiVersions = true;
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.DefaultApiVersion = new ApiVersion(1, 0);
}).AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddEndpointsApiExplorer();

// Set Documentation
builder.Services.AddSwaggerGen(options =>
{
    OpenApiSecurityScheme openApiSecurityScheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Por favor insira no campo o token JWT com Bearer.",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "oauth2"
    };
    options.AddSecurityDefinition("Bearer", openApiSecurityScheme);
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        openApiSecurityScheme,
        Array.Empty<string>()
    } });
});
builder.Services.ConfigureOptions<ConfigureSwaggerExtension>();

// App Configuration
var app = builder.Build();

// Add SwaggerUi
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = string.Empty;
    options.ConfigObject.DisplayRequestDuration = true;
    options.DocumentTitle = "Swagger - " + PlatformServices.Default.Application.ApplicationName.Split(".")[0];
    foreach (var description in app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
});

app.UseCors(builder => builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Creating tables
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<MySqlContext>();
    context.Database.EnsureCreated();
}

app.Run();