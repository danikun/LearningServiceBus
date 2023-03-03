using Microsoft.Extensions.Hosting;

Console.Title = "Bookings Service";

var builder = Host.CreateDefaultBuilder(args)
    .UseNServiceBus(_ =>
    {
        var endpointConfiguration = new EndpointConfiguration("Booking");
        endpointConfiguration.EnableInstallers();
    
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString("host=localhost;");
        transport.UseConventionalRoutingTopology(QueueType.Classic);

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.SendHeartbeatTo("Particular.Learning");

        var metrics = endpointConfiguration.EnableMetrics();
        metrics.SendMetricDataToServiceControl("Particular.Monitoring",
            TimeSpan.FromMilliseconds(500));

        return endpointConfiguration;
    });

await builder.RunConsoleAsync();