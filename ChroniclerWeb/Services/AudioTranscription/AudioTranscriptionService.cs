using System.Net.Http.Headers;
using System.Text.Json;

namespace ChroniclerWeb.Services.AudioTranscription
{
    public class AudioTranscriptionService : IAudioTranscriptionService
    {
        
            private readonly HttpClient _httpClient;
            private readonly IConfiguration _configuration;

            public AudioTranscriptionService(HttpClient httpClient, IConfiguration configuration)
            {
                _httpClient = httpClient;
                _configuration = configuration;
            }

            public async Task<string> TranscribeAudioAsync(
                Stream audioStream,
                string language = "auto",
                string fileName = "audio.webm",
                string contentType = "audio/webm")
            {
                var apiKey = _configuration["OpenAI:ApiKey"];
                if (string.IsNullOrWhiteSpace(apiKey))
                    throw new InvalidOperationException("Missing OpenAI:ApiKey configuration.");

                using var form = new MultipartFormDataContent();
                var streamContent = new StreamContent(audioStream);
                streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
                form.Add(streamContent, "file", fileName);
                form.Add(new StringContent("whisper-1"), "model");
                form.Add(new StringContent("json"), "response_format");

                if (!string.IsNullOrWhiteSpace(language) && language != "auto")
                {
                    var normalized = language.Split('-')[0].Trim().ToLowerInvariant();
                    form.Add(new StringContent(normalized), "language");
                }

                using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/audio/transcriptions");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                request.Content = form;

                using var response = await _httpClient.SendAsync(request);
                var raw = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException($"Transcription failed: {raw}");

                using var doc = JsonDocument.Parse(raw);
                if (doc.RootElement.TryGetProperty("text", out var textElement))
                    return textElement.GetString() ?? string.Empty;

                throw new InvalidOperationException("Invalid transcription response payload.");
            }
        
    }
}
