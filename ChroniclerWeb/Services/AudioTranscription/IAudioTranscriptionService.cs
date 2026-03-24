namespace ChroniclerWeb.Services.AudioTranscription
{
    public interface IAudioTranscriptionService
    {
        Task<string> TranscribeAudioAsync(Stream audioStream, string language = "en-US");
    }
}