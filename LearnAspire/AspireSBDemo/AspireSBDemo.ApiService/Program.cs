using Azure.Messaging.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.AddAzureServiceBusClient(connectionName: "pcad15");

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapPost("/notify", static async (ServiceBusClient client, string message) =>
{
    var sender = client.CreateSender("vcnotificationtopic");

    // Create a batch
    using ServiceBusMessageBatch messageBatch =
        await sender.CreateMessageBatchAsync();

    if (messageBatch.TryAddMessage(
            new ServiceBusMessage($"Message {message}")) is false)
    {
        // If it's too large for the batch.
        throw new Exception(
            $"The message {message} is too large to fit in the batch.");
    }

    // Use the producer client to send the batch of
    // messages to the Service Bus topic.
    await sender.SendMessagesAsync(messageBatch);

    Console.WriteLine($"A message has been published to the topic.");
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
