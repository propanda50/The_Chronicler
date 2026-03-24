using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services.AudioTranscription;

namespace ChroniclerWeb.Services.FileUpload
{
    public class FileUploadService : IFileUploadService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAudioTranscriptionService _audioTranscriptionService;

        public FileUploadService(ApplicationDbContext context, IAudioTranscriptionService audioTranscriptionService)
        {
            _context = context;
            _audioTranscriptionService = audioTranscriptionService;
        }

        public async Task<UploadedFile> UploadFileAsync(
            IFormFile file,
            string userId,
            int? campaignId = null,
            int? characterId = null,
            int? locationId = null,
            FileType fileType = FileType.Other)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileData = Convert.ToBase64String(memoryStream.ToArray());

            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                FileSize = file.Length,
                FileData = fileData,
                Url = $"data:{file.ContentType};base64,{fileData}",
                Type = fileType,
                UploadedById = userId,
                CampaignId = campaignId,
                CharacterId = characterId,
                LocationId = locationId,
                UploadedAt = DateTime.UtcNow
            };

            _context.UploadedFiles.Add(uploadedFile);
            await _context.SaveChangesAsync();

            return uploadedFile;
        }

        public async Task<string?> GetFileDataAsync(int fileId)
        {
            var file = await _context.UploadedFiles.FindAsync(fileId);
            return file?.FileData;
        }

        public async Task DeleteFileAsync(int fileId)
        {
            var file = await _context.UploadedFiles.FindAsync(fileId);
            if (file != null)
            {
                _context.UploadedFiles.Remove(file);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UploadedFile>> GetFilesByCampaignAsync(int campaignId)
        {
            return await Task.FromResult(_context.UploadedFiles
                .Where(f => f.CampaignId == campaignId)
                .OrderByDescending(f => f.UploadedAt)
                .AsEnumerable());
        }

        public async Task<IEnumerable<UploadedFile>> GetFilesByCharacterAsync(int characterId)
        {
            return await Task.FromResult(_context.UploadedFiles
                .Where(f => f.CharacterId == characterId)
                .OrderByDescending(f => f.UploadedAt)
                .AsEnumerable());
        }
    }
}
