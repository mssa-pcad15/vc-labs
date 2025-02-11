using System.Net.Http.Headers;

HttpClient client = new HttpClient();
var result = await GenerateSpeechAsync("https://ai900lab.openai.azure.com", "4CZQb0MDHqlr9Cvmg5UWReOSHwu8OzNLYvpDMK4bKGr1gAcYmVEOJQQJ99BBACHYHv6XJ3w3AAABACOGWmrp", "gpt-4o-mini-audio-preview", "output.wav");
Console.WriteLine(result);

async Task<string> GenerateSpeechAsync(string endpoint, string apiKey, string deploymentName, string speechFilePath)
{
    var requestUri = $"{endpoint}/openai/deployments/{deploymentName}/audio/speech?api-version=2025-01-01-preview";

    using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
    request.Headers.Add("apikey", apiKey);
        request.Content = new StringContent("""
                            {
                                "input": "Hi! What are you going to make?",
                                "voice": "fable",
                                "response_format": "mp3"
                            }
                            """);

    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
    var response = await client.SendAsync(request);
    response.EnsureSuccessStatusCode();

    var responseContent = await response.Content.ReadAsStringAsync();
    return responseContent;
}