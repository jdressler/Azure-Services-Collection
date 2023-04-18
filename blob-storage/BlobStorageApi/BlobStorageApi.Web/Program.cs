using Azure.Identity;
using BlobStorageApi.Contracts.Interfaces;
using BlobStorageApi.Services.Services;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IBlobService, BlobService>();

builder.Services.AddTransient<IContainerService, ContainerService>();

builder.Services.AddAzureClients(x =>
{
    x.AddBlobServiceClient(builder.Configuration["Azure:BlobKey"]);
    x.UseCredential(new DefaultAzureCredential());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureLogging(logging =>
{
    logging.AddLog4Net("log4net.config");
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
