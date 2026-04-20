using Azure.Identity;
using Azure.Storage.Blobs;
using Blobstorage.Interfaces;
using Blobstorage.Services;

var builder = WebApplication.CreateBuilder(args);

/*
 ============================================================
 Prerequisite:
 - An Azure Key Vault must already exist
 - It should contain a secret with your Blob Storage connection string
 ============================================================
*/

var keyVaultUrl = builder.Configuration["KeyVaultKey:KeyVaultURL"];

if (string.IsNullOrEmpty(keyVaultUrl))
{
    throw new InvalidOperationException("KeyVault URL missing");
}

// Connect to Azure Key Vault using DefaultAzureCredential
// Works with Azure CLI (local) or Managed Identity (Azure)
builder.Configuration.AddAzureKeyVault(
    new Uri(keyVaultUrl),
    new DefaultAzureCredential());

/*
 ============================================================
 Retrieve Blob Storage connection string from Azure Key Vault

 IMPORTANT:
 - Replace "your-secret-name" with the name of your Key Vault secret
 - The secret should contain your Blob Storage connection string

 Example:
 Secret name: your-secret-name
 Secret value: <your blob storage connection string>
 ============================================================
*/
var blobstorageConnectionString =
    builder.Configuration["your-secret-name"]; // <-- change this

if (string.IsNullOrWhiteSpace(blobstorageConnectionString))
{
    throw new Exception(
        "Missing Blob Storage connection string. " +
        "Check your Key Vault secret name and configuration.");
}

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Register BlobServiceClient using connection string from Key Vault
builder.Services.AddSingleton(new BlobServiceClient(blobstorageConnectionString));

/*
 ============================================================
 Register your Blob service

 Replace "containername" with your own container name
 ============================================================
*/
builder.Services.AddScoped<IBlobservice>(sp =>
{
    var blobClient = sp.GetRequiredService<BlobServiceClient>();
    return new BlobService(blobClient, "containername"); // <-- change if needed
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "API is running 🚀");

app.Run();