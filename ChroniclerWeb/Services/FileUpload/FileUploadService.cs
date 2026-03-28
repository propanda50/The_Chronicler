using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services.AudioTranscription;
using Microsoft.EntityFrameworkCore;

namespace ChroniclerWeb.Services.FileUpload
{
    public class FileUploadService : IFileUploadService
    {
        private readonly ApplicationDbContext _context;

        public FileUploadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UploadedFile> UploadFileAsync(
            IFormFile file,
            string userId,
            int? campaignId = null,
            int? characterId = null,
            int? locationId = null,
            int? eventId = null,
            int? sessionId = null,
            int? forumPostId = null,
            FileType fileType = FileType.Other,
            string? description = null)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var base64 = Convert.ToBase64String(memoryStream.ToArray());

            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                FileSize = file.Length,
                FileData = base64,
                Url = $"data:{file.ContentType};base64,{base64}",
                Type = fileType,
                Description = description,
                UploadedById = userId,
                CampaignId = campaignId,
                CharacterId = characterId,
                LocationId = locationId,
                EventId = eventId,
                SessionId = sessionId,
                ForumPostId = forumPostId,
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
            return await _context.UploadedFiles
                .Include(f => f.UploadedBy)
                .Include(f => f.Character)
                .Include(f => f.Location)
                .Include(f => f.Event)
                .Include(f => f.Session)
                .Include(f => f.ForumPost)
                .Where(f => f.CampaignId == campaignId)
                .OrderByDescending(f => f.UploadedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<UploadedFile>> GetFilesByCharacterAsync(int characterId)
        {
            return await _context.UploadedFiles
                .Include(f => f.UploadedBy)
                .Where(f => f.CharacterId == characterId)
                .OrderByDescending(f => f.UploadedAt)
                .ToListAsync();
        }
    }
}
