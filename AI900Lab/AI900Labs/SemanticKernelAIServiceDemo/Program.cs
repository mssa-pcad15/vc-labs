using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using System;


IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddAzureOpenAIChatCompletion(
    deploymentName: "gpt-4o",
    apiKey: "4CZQb0MDHqlr9Cvmg5UWReOSHwu8OzNLYvpDMK4bKGr1gAcYmVEOJQQJ99BBACHYHv6XJ3w3AAABACOGWmrp",
    endpoint: "https://ai900lab.openai.azure.com/",
    modelId: "gpt-4", // Optional name of the underlying model if the deployment name doesn't match the model name
    httpClient: new HttpClient() // Optional; if not provided, the HttpClient from the kernel will be used
);
Kernel kernelGpt4o = kernelBuilder.Build();
var chatCompletionService_Gpt4 = kernelGpt4o.GetRequiredService<IChatCompletionService>();

AzureOpenAIChatCompletionService standaloneChatCompletion_gpt35 = new(
    deploymentName: "gpt-35-turbo",
 apiKey: "4CZQb0MDHqlr9Cvmg5UWReOSHwu8OzNLYvpDMK4bKGr1gAcYmVEOJQQJ99BBACHYHv6XJ3w3AAABACOGWmrp",
    endpoint: "https://ai900lab.openai.azure.com/",
    modelId: "gpt-35-turbo", // Optional name of the underlying model if the deployment name doesn't match the model name
    httpClient: new HttpClient() // Optional; if not provided, the HttpClient from the kernel will be used
);



ChatHistory history = [];
history.AddUserMessage("Famous Proverb from Bruce Lee?}");

var response = await chatCompletionService_Gpt4.GetChatMessageContentAsync(
    history,
    kernel: kernelGpt4o
);
Console.WriteLine("***********Buffered Response From GPT-4*****************");
Console.WriteLine($"{response.ToString()}");

var responseStream = chatCompletionService_Gpt4.GetStreamingChatMessageContentsAsync(
    chatHistory: history,
    kernel: kernelGpt4o
);
Console.WriteLine("***********Stream Response From GPT-4*****************");
await foreach (var chunk in responseStream)
{
    Console.Write(chunk);
}



Console.WriteLine("");

var responseStandAlone = await standaloneChatCompletion_gpt35.GetChatMessageContentAsync(
    history
);
Console.WriteLine("***********Buffered Response From GPT-3.5*****************");
Console.WriteLine($"{response.ToString()}");

var responseStandAloneStream = standaloneChatCompletion_gpt35.GetStreamingChatMessageContentsAsync(
    chatHistory: history
);
Console.WriteLine("***********Stream Response From GPT-3.5*****************");
await foreach (var chunk in responseStandAloneStream)
{
    Console.Write(chunk);
}