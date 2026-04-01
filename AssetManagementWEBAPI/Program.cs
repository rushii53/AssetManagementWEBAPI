using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Repository;
using AssetManagementWEBAPI.Service;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Grafana.Loki;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog early
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(new CompactJsonFormatter())
    // Optional file sink for persisted logs (daily rolling)
    .WriteTo.File(new CompactJsonFormatter(), "logs/assetmanagement-service-.json", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, shared: true, rollingInterval: RollingInterval.Day)
    .WriteTo.GrafanaLoki(
        "http://localhost:3100",
        new[]
        {
            new LokiLabel { Key = "app", Value = "assetmanagement-service" },
        },
        textFormatter: new RenderedCompactJsonFormatter()
    )
    .CreateLogger();

// Set the global logger
Log.Logger = logger;

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DBModel>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<TextFileModel>(builder.Configuration.GetSection("TextFilePath"));
builder.Services.AddSingleton<IMachineRepository, MachineTextFileRepository>();
builder.Services.AddSingleton<Serilog.ILogger>(_ => logger);
builder.Services.AddScoped<IMachineService, MachineService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen(option =>
{
    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentFilePath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
    option.IncludeXmlComments(xmlCommentFilePath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add correlation id middleware and Serilog request logging
app.UseMiddleware<AssetManagementWEBAPI.Middleware.CorrelationIdMiddleware>();
app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        if (httpContext.Request.Headers.TryGetValue("X-Correlation-ID", out var cid))
        {
            diagnosticContext.Set("CorrelationId", cid.ToString());
        }
    };
});

app.UseAuthorization();

app.MapControllers();

app.Run();
