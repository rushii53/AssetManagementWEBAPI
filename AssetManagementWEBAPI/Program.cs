using AssetManagementWEBAPI.DataContext;
using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Repository;
using AssetManagementWEBAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DBModel>(builder.Configuration.GetSection("AssetManagementDataStore"));

builder.Services.AddSingleton<IFileScanner>(option => new TextFileScannerAndParser("C:\\Users\\Bart_rus.GLOBAL\\Documents\\AssetFile.txt"));
builder.Services.AddSingleton<IMachineRepository,MachineRepository>();
builder.Services.AddSingleton<IMachineService,MachineService>();
builder.Services.AddSingleton<MongoDbAssetManagement>();
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
