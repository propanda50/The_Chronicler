using ChroniclerWeb.Models;

namespace ChroniclerWeb.Services.FileUpload
{
    public interface IFileUploadService
    {
        Task<UploadedFile> UploadFileAsync(
            IFormFile file,
            string userId,
            int? campaignId = null,
            int? characterId = null,
            int? locationId = null,
            int? eventId = null,
            int? sessionId = null,
            int? forumPostId = null,
            FileType fileType = FileType.Other,
            string? description = null);

        Task<string?> GetFileDataAsync(int fileId);
        Task DeleteFileAsync(int fileId);
        Task<IEnumerable<UploadedFile>> GetFilesByCampaignAsync(int campaignId);
        Task<IEnumerable<UploadedFile>> GetFilesByCharacterAsync(int characterId);
    }
}