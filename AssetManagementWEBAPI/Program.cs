using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Repository;
using AssetManagementWEBAPI.Service;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Sinks.OpenTelemetry;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region OTel configuration

#region Logging
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("service.name", "asset-management-service")
    .Enrich.WithProperty("service.version", "1.0.0")
    .WriteTo.Console()
    .WriteTo.OpenTelemetry(options =>
    {
        options.RestrictedToMinimumLevel = Serilog.Events.LogEventLevel.Information;
        options.Endpoint = "http://localhost:4318/v1/logs"; // HTTP, not gRPC
        options.Protocol = OtlpProtocol.HttpProtobuf;
        options.ResourceAttributes = new Dictionary<string, object>
        {
            ["service.name"] = "asset-management-service",
            ["service.version"] = "1.0.0"
        };
    })
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Host.UseSerilog();
#endregion

#region Trace config

var resourceBuilder = ResourceBuilder.CreateDefault()
    .AddService(serviceName: "asset-management-service", serviceVersion: "1.0.0");

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .SetResourceBuilder(resourceBuilder)
        .AddAspNetCoreInstrumentation(options =>
        {
            options.RecordException = true;
        })
        .AddHttpClientInstrumentation()
        .SetSampler(new ParentBasedSampler(new TraceIdRatioBasedSampler(1.0))) // adjust later
        .AddOtlpExporter(otlp =>
        {
            otlp.Endpoint = new Uri("http://localhost:4317"); // FIXED
            otlp.Protocol = OtlpExportProtocol.Grpc;
        })
    );
#endregion
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DBModel>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<TextFileModel>(builder.Configuration.GetSection("TextFilePath"));
builder.Services.AddSingleton<IMachineRepository, MachineTextFileRepository>();
builder.Services.AddScoped<IMachineService, MachineService>();
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

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

app.UseAuthorization();

app.MapControllers();

app.Run();
