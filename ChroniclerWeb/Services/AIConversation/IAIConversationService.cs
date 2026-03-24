namespace ChroniclerWeb.Services.AIConversation
{
    public interface IAIConversationService
    {
        Task<string> GenerateImageAsync(string prompt, string context = "");
        Task<AIResponse> SendMessageAsync(string message, string? userId = null, int? campaignId = null);
    }
}