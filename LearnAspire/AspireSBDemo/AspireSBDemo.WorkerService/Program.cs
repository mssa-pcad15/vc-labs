
using AspireSBDemo.WorkerService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddAzureServiceBusClient(connectionName: "pcad15");

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();