using AssetManagementBlazor.Components;
using AssetManagementBlazor.Service;
using AssetManagementBlazor.MessageHandlers;
using Serilog;
using Serilog.Formatting.Compact;
using System.Net.Http.Headers;
using Serilog.Sinks.Grafana.Loki;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog early
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(new CompactJsonFormatter())
    .WriteTo.File(new CompactJsonFormatter(), "logs/assetmanagement-client-.json", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, shared: true, rollingInterval: RollingInterval.Day)

    .WriteTo.GrafanaLoki(
        "http://localhost:3100",
        new[]
        {
            new LokiLabel { Key = "app", Value = "assetmanagement-client" },
        },
        textFormatter: new RenderedCompactJsonFormatter()
    )
    .CreateLogger();

builder.Host.UseSerilog();

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
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}).AddHttpMessageHandler<CorrelationIdForwardingHandler>();

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
