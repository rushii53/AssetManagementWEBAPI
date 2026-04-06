using AssetManagementBlazor.Components;
using AssetManagementBlazor.Service;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Sinks.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

var resourceBuilder = ResourceBuilder.CreateDefault()
    .AddService(serviceName: "asset-management-app", serviceVersion: "1.0.0");

#region Logging
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.OpenTelemetry(options =>
    {
        options.RestrictedToMinimumLevel = Serilog.Events.LogEventLevel.Information;
        options.Endpoint = "http://localhost:4318/v1/logs"; // HTTP, not gRPC
        options.Protocol = OtlpProtocol.HttpProtobuf;
        options.ResourceAttributes = new Dictionary<string, object>
        {
            ["service.name"] = "asset-management-app",
            ["service.version"] = "1.0.0"
        };
    })
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Host.UseSerilog();


builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("asset-management-app"))
    .WithTracing(tracing => tracing
        .SetResourceBuilder(resourceBuilder)
        .AddAspNetCoreInstrumentation(options =>
        {
            options.RecordException = true;
        })
        .AddHttpClientInstrumentation() // auto injects trace context into outgoing HTTP calls
        .SetSampler(new ParentBasedSampler(new TraceIdRatioBasedSampler(1.0))) // adjust later
        .AddOtlpExporter(otlp =>
        {
            otlp.Endpoint = new Uri("http://localhost:4317"); // FIXED
            otlp.Protocol = OtlpExportProtocol.Grpc;
        })
    );
#endregion

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpContextAccessor();

//Dependency Injection
builder.Services.AddTransient<CorrelationIdForwardingHandler>();

builder.Services.AddHttpClient<IMachineService, MachineService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7130");
    // ensure we send correlation id header if present in ambient call context
    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);
builder.Services.AddScoped<IMachineService, MachineService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// Add middleware for correlation id
app.UseMiddleware<AssetManagementBlazor.Middleware.CorrelationIdMiddleware>();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
