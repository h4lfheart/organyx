using System.Text.Json.Serialization;
using DotNetEnv;
using Organyx.Application;
using Organyx.Infrastructure;
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

builder.WebHost.UseUrls($"http://127.0.0.1:{backendPort}");

builder.Services.AddSerilog((services, config) => config
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .WriteTo.Console());

builder.Services.AddOpenApi();

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddApplicationPart(typeof(ApplicationIocConfiguration).Assembly);

builder.Services.AddApplicationIoc();
builder.Services.AddInfrastructureIoc();

var app = builder.Build();

var supabaseService = app.Services.GetRequiredService<SupabaseService>();
await supabaseService.InitializeAsync();

app.UseSerilogRequestLogging();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Title = "Organyx API";
    options.DefaultHttpClient = new KeyValuePair<ScalarTarget, ScalarClient>(ScalarTarget.CSharp, ScalarClient.HttpClient);
    options.Layout = ScalarLayout.Classic;
});

app.MapControllers();

app.Run();