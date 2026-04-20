using Blobstorage.DTO;

namespace Blobstorage.Interfaces
{
    public interface IBlobservice
    {

        Task<List<DtoFile>> GetAllFilesAsync();
        Task<(Stream Content, string ContentType, string FileName)> GetFileAsync(string fileName);

    }
}
