using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Blobstorage.DTO;
using Blobstorage.Interfaces;

namespace Blobstorage.Services
{
    /*
     ============================================================
     Service responsible for interacting with Azure Blob Storage

     - Uses BlobServiceClient (injected via DI)
     - Connects to a specific container
     - Handles listing and downloading files
     ============================================================
    */
    public class BlobService : IBlobservice
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobService(BlobServiceClient blobServiceClient, string containerName)
        {
            _blobServiceClient = blobServiceClient;
            _containerName = containerName;
        }

        // Returns metadata for all files in the container
        public async Task<List<DtoFile>> GetAllFilesAsync()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            var files = new List<DtoFile>();

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                files.Add(new DtoFile
                {
                    FileName = blobItem.Name,
                    ContentType = blobItem.Properties.ContentType,
                    Size = blobItem.Properties.ContentLength ?? 0
                });
            }

            return files;
        }

        // Downloads a file from Blob Storage by file name
        public async Task<(Stream Content, string ContentType, string FileName)> GetFileAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            var blobClient = containerClient.GetBlobClient(fileName);

            if (!await blobClient.ExistsAsync())
                throw new FileNotFoundException();

            var response = await blobClient.DownloadAsync();

            return (
                response.Value.Content,
                response.Value.Details.ContentType,
                fileName
            );
        }
    }
}