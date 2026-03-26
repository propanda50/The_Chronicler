namespace TheChronicler.Web.Services
{
    public interface IAudioTranscriptionService
    {
        Task<string> TranscribeAudioAsync(Stream audioStream, string language = "en-US");
    }

    public class AudioTranscriptionService : IAudioTranscriptionService
    {
        public async Task<string> TranscribeAudioAsync(Stream audioStream, string language = "en-US")
        {
            // Note: Audio transcription requires external service integration
            // Options: Azure Cognitive Services, Google Cloud Speech, AWS Transcribe, or Whisper API
            // This is a placeholder implementation
            
            await Task.CompletedTask;
            
            return "[Audio transcription requires external service. Please configure Azure, Google, or OpenAI Whisper API in appsettings.json]";
        }
    }
}
