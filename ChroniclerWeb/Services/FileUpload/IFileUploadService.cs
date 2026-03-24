using ChroniclerWeb.Models;

namespace ChroniclerWeb.Services.FileUpload
{
    public interface IFileUploadService
    {
        Task DeleteFileAsync(int fileId);
        Task<string?> GetFileDataAsync(int fileId);
        Task<IEnumerable<UploadedFile>> GetFilesByCampaignAsync(int campaignId);
        Task<IEnumerable<UploadedFile>> GetFilesByCharacterAsync(int characterId);
        Task<UploadedFile> UploadFileAsync(IFormFile file, string userId, int? campaignId = null, int? characterId = null, int? locationId = null, FileType fileType = FileType.Other);
    }
}