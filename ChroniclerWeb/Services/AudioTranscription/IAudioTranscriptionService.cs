namespace ChroniclerWeb.Services.AudioTranscription
{
    public interface IAudioTranscriptionService
    {
       
            Task<string> TranscribeAudioAsync(
                Stream audioStream,
                string language = "auto",
                string fileName = "audio.webm",
                string contentType = "audio/webm");
        
    }
}