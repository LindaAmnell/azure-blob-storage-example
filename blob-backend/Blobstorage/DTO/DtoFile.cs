namespace Blobstorage.DTO
{
    /*
     ============================================================
     Data Transfer Object (DTO) for files stored in Blob Storage

     Why this exists:
     - We do NOT return Azure Blob objects directly
     - We only expose the data the frontend needs
     - Keeps the API response clean and controlled

     Used when listing files from the container
     ============================================================
    */
    public class DtoFile
    {
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long Size { get; set; }
    }
}