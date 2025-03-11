using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var serviceBus = builder.AddAzureServiceBus("pcad15");
var vctopic = serviceBus.AddServiceBusTopic("vctopic", "vcnotificationtopic");
var vcsub = vctopic.AddServiceBusSubscription("vcsub", "vcmobile");
var apiService = builder.AddProject<Projects.AspireSBDemo_ApiService>("apiservice")
    .WithReference(serviceBus)
    .WaitFor(serviceBus);

builder.AddProject<Projects.AspireSBDemo_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.AspireSBDemo_WorkerService>("aspiresbdemo-workerservice")
    .WithReference(serviceBus).WaitFor(serviceBus)
    .WithReference(vcsub).WaitFor(vcsub);

builder.Build().Run();
