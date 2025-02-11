using Azure;
using Azure.AI.OpenAI;
using Azure.Identity; // Required for Passwordless auth

var endpoint = new Uri("https://ai900lab.openai.azure.com/");
var credentials = new AzureKeyCredential("5fXDgluICXIdSM7gJrpXZDSVFkYGPDtEYeLsN8XokfxbyeFQBjYRJQQJ99BBACHYHv6XJ3w3AAABACOGRXxI");
// var credentials = new DefaultAzureCredential(); // Use this line for Passwordless auth
var deploymentName = "whisper"; // Default deployment name, update with your own if necessary
var audioFilePath = @"Audio/sample.wav";

var openAIClient = new AzureOpenAIClient(endpoint, credentials);

var audioClient = openAIClient.GetAudioClient(deploymentName);

var result = await audioClient.TranscribeAudioAsync(audioFilePath);

Console.WriteLine("Transcribed text:");
foreach (var item in result.Value.Text)
{
    Console.Write(item);
}

