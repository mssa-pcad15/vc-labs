﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AppLifetime.Example;

public sealed class ExampleHostedService : IHostedService, IHostedLifecycleService
{
    private readonly ILogger _logger;

    public ExampleHostedService(
        ILogger<ExampleHostedService> logger,
        IHostApplicationLifetime appLifetime)
    {
        _logger = logger;

        appLifetime.ApplicationStarted.Register(OnStarted);
        appLifetime.ApplicationStopping.Register(OnStopping);
        appLifetime.ApplicationStopped.Register(OnStopped);
    }

    Task IHostedLifecycleService.StartingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("1. StartingAsync has been called. the host is about to start.");

        return Task.CompletedTask;
    }

    Task IHostedService.StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("2. StartAsync has been called.");

        return Task.CompletedTask;
    }

    Task IHostedLifecycleService.StartedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("3. StartedAsync has been called. The Host has completed the startup routine");

        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        //this is the main logic
        //every 30minutes, check if website is still working
        // or sign up to be notified of something
        _logger.LogInformation("4. OnStarted has been called.");
    }

    private void OnStopping()
    {
        // unsubscribe to things
        _logger.LogInformation("5. OnStopping has been called.");
    }

    Task IHostedLifecycleService.StoppingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("6. StoppingAsync has been called.");

        return Task.CompletedTask;
    }

    Task IHostedService.StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("7. StopAsync has been called.");

        return Task.CompletedTask;
    }

    Task IHostedLifecycleService.StoppedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("8. StoppedAsync has been called.");

        return Task.CompletedTask;
    }

    private void OnStopped()
    {
        _logger.LogInformation("9. OnStopped has been called.");
    }
}