using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Repository;
using AssetManagementWEBAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DBModel>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<TextFileModel>(builder.Configuration.GetSection("TextFilePath"));
builder.Services.AddSingleton<IMachineRepository,MachineTextFileRepository>();
builder.Services.AddSingleton<IMachineService,MachineService>();
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
