using System.Text.Json.Serialization;
using DotNetEnv;
using Organyx.Api;
using Organyx.Development;
using Organyx.Infrastructure;
using Organyx.Infrastructure.Errors;
using Organyx.Infrastructure.Validation;
using Organyx.Infrastructure.Services;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateBootstrapLogger();

if (!Env.TraversePath().Load(".env.local").Any())
    throw new InvalidOperationException(
        "Missing .env.local. Copy .env.example to .env.local at the repo root and adjust if needed.");

var builder = WebApplication.CreateBuilder(args);

var backendPort = Environment.GetEnvironmentVariable("BACKEND_PORT")
    ?? throw new InvalidOperationException("BACKEND_PORT is required. Set it in .env.local.");
var frontendPort = Environment.GetEnvironmentVariable("FRONTEND_PORT")
    ?? throw new InvalidOperationException("FRONTEND_PORT is required. Set it in .env.local.");

builder.WebHost.UseUrls($"http://127.0.0.1:{backendPort}");

builder.Services.AddSerilog((services, config) => config
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .WriteTo.Console());

builder.Services.AddProblemDetails();
builder.Services.AddOrganyxOpenApi();

builder.Services
    .AddControllers(o =>  o.Filters.Add<FluentValidationActionFilter>())
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddApplicationPart(typeof(DevelopmentIocConfiguration).Assembly);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(
                $"http://127.0.0.1:{frontendPort}",
                $"http://localhost:{frontendPort}")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddDevelopmentIoc();
builder.Services.AddInfrastructureIoc();

var app = builder.Build();

var supabaseService = app.Services.GetRequiredService<SupabaseService>();
await supabaseService.InitializeAsync();

app.UseExceptionHandler(new ExceptionHandlerOptions
{
    StatusCodeSelector = ex => ex switch
    {
        NotFoundException => StatusCodes.Status404NotFound,
        ConflictException => StatusCodes.Status409Conflict,
        BusinessRuleException => StatusCodes.Status400BadRequest,
        BadHttpRequestException e => e.StatusCode,
        _ => StatusCodes.Status500InternalServerError
    }
});
app.UseStatusCodePages();

app.UseSerilogRequestLogging();
app.UseCors();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Title = "Organyx API";
    options.DefaultHttpClient = new KeyValuePair<ScalarTarget, ScalarClient>(ScalarTarget.CSharp, ScalarClient.HttpClient);
    options.Layout = ScalarLayout.Classic;
    options.AddOrganyxDocuments();
});

app.MapControllers();

app.Run();
