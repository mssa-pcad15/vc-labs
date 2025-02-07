using Microsoft.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Kernel = Microsoft.SemanticKernel.Kernel;
using System.Text;
using System.Text.Json;
using Microsoft.SemanticKernel.Connectors.OpenAI;



ILoggerFactory myLoggerFactory = NullLoggerFactory.Instance;
var builder = Kernel.CreateBuilder(); //get a kernel builder
builder.Services.AddSingleton(myLoggerFactory); // add a logger to the kernel
builder // add Chat AI to the kernel
.AddAzureOpenAIChatCompletion(
    "gpt-4o-mini",                   // Azure OpenAI *Deployment Name*
    "https://pcad15-oai457466800731.openai.azure.com/",    // Azure OpenAI *Endpoint*
    "98VZEOTer5tKu4W6vIRxpXk75DxNF9lYKvcbdPap5eVOQo9SYpaxJQQJ99BAACHYHv6XJ3w3AAAAACOG1BJ0"
);

var kernel = builder.Build(); // build the kernel
                              // now we need the kernel to parse plugins folder and create corresponding
                              // function to invoke.


//Notebook 4:
//Chatbot that remembers conversation history
const string skPrompt = @"
ChatBot can have a conversation with you about any topic.
It can give explicit instructions or say 'I don't know' if it does not have an answer.

{{$history}}
User: {{$userInput}}
ChatBot:";

var executionSettings = new OpenAIPromptExecutionSettings
{
    MaxTokens = 2000,
    Temperature = 0.7,
    TopP = 0.5
};

var chatFunction = kernel.CreateFunctionFromPrompt(skPrompt, executionSettings); //this is the abbreviated version from notebook 3
var history = "";

//the kernel argument is used to pass the history to the function
var arguments = new KernelArguments()
{
    ["history"] = history
};

Func<string, Task> Chat = async (string input) => {
    // Save new message in the arguments
    arguments["userInput"] = input;

    // Process the user message and get an answer
    var answer = await chatFunction.InvokeAsync(kernel, arguments);

    // Append the new interaction to the chat history
    var result = $"\nUser: {input}\nAI: {answer}\n";
    history += result;

    arguments["history"] = history;

    // Show the response
    Console.WriteLine(result);
};

do {
    Console.WriteLine("What do you want to say?(q to quit, h to print history)");
    var userInput = Console.ReadLine();
    if (userInput == "q")
    {
        break;
    }

    if (userInput == "h")
    {
        Console.WriteLine(history);
        continue;
    }

    await Chat(userInput);
} while (true);


//Notebook 2:
//await UsePlugInFromDirectory(kernel);
//Notebook 3:
//await DynamicInvokeKernelFunction(kernel);

static async Task UsePlugInFromDirectory(Kernel kernel)
{
    var funPluginDirectoryPath =
        Path.Combine(System.IO.Directory.GetCurrentDirectory(), "plugins");

    Console.WriteLine($"parsing plugin from {funPluginDirectoryPath}");

    KernelPlugin funPluginFunctions = kernel.ImportPluginFromPromptDirectory(funPluginDirectoryPath);

    foreach (var function in funPluginFunctions)
    {
        Console.WriteLine($"""
        function: {function.Name}
           Description: {function.Description}
           ExecutionSettings: {JsonSerializer.Serialize(function.ExecutionSettings)}
        """);

        Console.WriteLine("What is your birthday?");

        string birthDate = Console.ReadLine();

        var arguments = new KernelArguments() { ["birthDate"] = birthDate };

        var task = kernel.InvokeAsync(function, arguments);
        Console.Write("Finding your fortune.");
        while (!task.IsCompleted)
        {
            Console.Write(".");
            await Task.Delay(1000);
        }
        Console.WriteLine($"{task.Result}");
    }
}

static async Task DynamicInvokeKernelFunction(Kernel kernel)
{
    //this uses inline function
    string skPrompt = """
{{$input}}

Summarize the content above in 30 words or less.
""";

    float temp = new Random().Next(0, 100) / 100.0f;
    var executionSettings = new OpenAIPromptExecutionSettings
    {
        MaxTokens = 2000,
        Temperature = temp,
        TopP = 0.5
    };
    var promptTemplateConfig = new PromptTemplateConfig(skPrompt);
    promptTemplateConfig.ExecutionSettings.Add("RandomExecution", executionSettings);

    var promptTemplateFactory = new KernelPromptTemplateFactory();
    var promptTemplate = promptTemplateFactory.Create(promptTemplateConfig);

    var renderedPrompt = await promptTemplate.RenderAsync(kernel);

    Console.WriteLine(renderedPrompt);

    var summaryFunction = kernel.CreateFunctionFromPrompt(skPrompt, executionSettings);

    var input = """
Demo (ancient Greek poet)
From Wikipedia, the free encyclopedia
Demo or Damo (Greek: Δεμώ, Δαμώ; fl. c. AD 200) was a Greek woman of the Roman period, known for a single epigram, engraved upon the Colossus of Memnon, which bears her name. She speaks of herself therein as a lyric poetess dedicated to the Muses, but nothing is known of her life.[1]
Identity
Demo was evidently Greek, as her name, a traditional epithet of Demeter, signifies. The name was relatively common in the Hellenistic world, in Egypt and elsewhere, and she cannot be further identified. The date of her visit to the Colossus of Memnon cannot be established with certainty, but internal evidence on the left leg suggests her poem was inscribed there at some point in or after AD 196.[2]
Epigram
There are a number of graffiti inscriptions on the Colossus of Memnon. Following three epigrams by Julia Balbilla, a fourth epigram, in elegiac couplets, entitled and presumably authored by "Demo" or "Damo" (the Greek inscription is difficult to read), is a dedication to the Muses.[2] The poem is traditionally published with the works of Balbilla, though the internal evidence suggests a different author.[1]
In the poem, Demo explains that Memnon has shown her special respect. In return, Demo offers the gift for poetry, as a gift to the hero. At the end of this epigram, she addresses Memnon, highlighting his divine status by recalling his strength and holiness.[2]
Demo, like Julia Balbilla, writes in the artificial and poetic Aeolic dialect. The language indicates she was knowledgeable in Homeric poetry—'bearing a pleasant gift', for example, alludes to the use of that phrase throughout the Iliad and Odyssey.[a][2] 
""";

    var summaryResult = await kernel.InvokeAsync(summaryFunction, new() { ["input"] = input });
    Console.WriteLine("*****************************");
    Console.WriteLine(summaryResult);
    Console.WriteLine("*****************************");

    await Task.Delay(5000);
    //abbreviated version
    var result = await kernel.InvokePromptAsync(skPrompt, new() { ["input"] = input });
    Console.WriteLine("------------------------------");
    Console.WriteLine(result);
    Console.WriteLine("------------------------------");
    //await UsePlugInFromDirectory(kernel);
}