var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var cache2 = builder.AddRedis("cache2");

var apiService = builder.AddProject<Projects.AspireSample_ApiService>("apiservice");

builder.AddProject<Projects.AspireSample_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(cache2)
    .WaitFor(cache2)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
