using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using OpenAI.Chat; // Required for Passwordless auth

Console.WriteLine(new string('*',80));
Console.WriteLine("Vision Chat with Remote URL and Key Auth");
//await VisionChatWithKeyAuthRemoteURL();


Console.WriteLine(new string('*', 80));
Console.WriteLine("Vision Chat with Local Binary Data and Key Auth");
//await VisionChatWithKeyAuthByteData();


Console.WriteLine(new string('*', 80));
Console.WriteLine("Vision Chat with Remote URL and Default Credential");
await VisionChatWithDefaultCredentialRemoteURL();

Console.WriteLine(new string('*', 80));
Console.WriteLine("Vision Chat with Local Binary Data and Default Credential");
await VisionChatWithDefaultCredentialByteData();
static async Task VisionChatWithKeyAuthRemoteURL()
{
    var endpoint = new Uri("https://ai900lab.openai.azure.com/");
    var credentials = new AzureKeyCredential("65VFGsSk3KtG83qixotloTXm6KdxwhoMdsa8ynYL58YNGoIaDJNyJQQJ99BBACHYHv6XJ3w3AAABACOGlXvE");
    // var credentials = new DefaultAzureCredential(); // Use this line for Passwordless auth
    var deploymentName = "gpt-4o"; // Default name, update with your own if needed

    var openAIClient = new AzureOpenAIClient(endpoint, credentials);
    var chatClient = openAIClient.GetChatClient(deploymentName);

    var imageUri = "https://vc0316.blob.core.windows.net/ai900/test1.jpg";

    List<ChatMessage> messages = [
        new UserChatMessage(
        ChatMessageContentPart.CreateTextPart("Please describe the following image:"),
        ChatMessageContentPart.CreateImagePart(new Uri(imageUri), imageDetailLevel: ChatImageDetailLevel.Auto))
    ];

    ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages);

    Console.WriteLine($"[ASSISTANT]:");
    Console.WriteLine($"{chatCompletion.Content[0].Text}");
}

static async Task VisionChatWithKeyAuthByteData()
{
    var endpoint = new Uri("https://ai900lab.openai.azure.com/");
    var credentials = new AzureKeyCredential("65VFGsSk3KtG83qixotloTXm6KdxwhoMdsa8ynYL58YNGoIaDJNyJQQJ99BBACHYHv6XJ3w3AAABACOGlXvE");
    // var credentials = new DefaultAzureCredential(); // Use this line for Passwordless auth
    var deploymentName = "gpt-4o"; // Default name, update with your own if needed

    var openAIClient = new AzureOpenAIClient(endpoint, credentials);
    var chatClient = openAIClient.GetChatClient(deploymentName);

    byte[] imageBytes = await File.ReadAllBytesAsync(@"image/test1.jpg");

    List<ChatMessage> messages = new List<ChatMessage>
    {
        new UserChatMessage(
        ChatMessageContentPart.CreateTextPart("Please describe the following image:"),
        ChatMessageContentPart.CreateImagePart(new BinaryData(imageBytes),"image/jpg", ChatImageDetailLevel.Auto))
    };

    ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages);

    Console.WriteLine($"[ASSISTANT]:");
    Console.WriteLine($"{chatCompletion.Content[0].Text}");
}

static async Task VisionChatWithDefaultCredentialRemoteURL()
{
    var endpoint = new Uri("https://ai900lab.openai.azure.com/");
    var credentials = new InteractiveBrowserCredential(new InteractiveBrowserCredentialOptions { TenantId = "75202359-8ca2-4185-af85-e8d288e60729" }); // Use this line for Passwordless auth
    var deploymentName = "gpt-4o"; // Default name, update with your own if needed

    var openAIClient = new AzureOpenAIClient(endpoint, credentials);
    var chatClient = openAIClient.GetChatClient(deploymentName);

    var imageUri = "https://vc0316.blob.core.windows.net/ai900/test1.jpg";

    List<ChatMessage> messages = new List<ChatMessage>
    {
        new UserChatMessage(
        ChatMessageContentPart.CreateTextPart("Please describe the following image:"),
        ChatMessageContentPart.CreateImagePart(new Uri(imageUri), imageDetailLevel: ChatImageDetailLevel.Auto))
    };

    ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages);

    Console.WriteLine($"[ASSISTANT]:");
    Console.WriteLine($"{chatCompletion.Content[0].Text}");
}

static async Task VisionChatWithDefaultCredentialByteData()
{
    var endpoint = new Uri("https://ai900lab.openai.azure.com/");
    var credentials = new InteractiveBrowserCredential( new InteractiveBrowserCredentialOptions { TenantId= "75202359-8ca2-4185-af85-e8d288e60729" }); // Use this line for Passwordless auth
    var deploymentName = "gpt-4o"; // Default name, update with your own if needed

    var openAIClient = new AzureOpenAIClient(endpoint, credentials);
    var chatClient = openAIClient.GetChatClient(deploymentName);

    byte[] imageBytes = await File.ReadAllBytesAsync(@"image/test1.jpg");

    List<ChatMessage> messages = new List<ChatMessage>
    {
        new UserChatMessage(
        ChatMessageContentPart.CreateTextPart("Please describe the following image:"),
        ChatMessageContentPart.CreateImagePart(new BinaryData(imageBytes), "image/jpg", ChatImageDetailLevel.Auto))
    };

    ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages);

    Console.WriteLine($"[ASSISTANT]:");
    Console.WriteLine($"{chatCompletion.Content[0].Text}");
}