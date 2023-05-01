using Azure.Identity;
using AzureADApi.Models.Models.Configuration;
using AzureADApi.Services.Interfaces;
using AzureADApi.Services.Services;
using Microsoft.Graph;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddSingleton(new AppConfiguration(builder.Configuration["Azure:MailDomain"]));

var options = new TokenCredentialOptions
{
    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
};

var clientSecretCredential = new ClientSecretCredential(
    builder.Configuration["Azure:TenantId"], builder.Configuration["Azure:ClientId"], builder.Configuration["Azure:ClientSecret"], options);
var scopes = new[] { "https://graph.microsoft.com/.default" };
builder.Services.AddSingleton(new GraphServiceClient(clientSecretCredential, scopes));
builder.Services.AddTransient<IUserService, UserService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
