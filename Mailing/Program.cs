using Messages;
using NServiceBus;
using NServiceBus.Logging;

Console.Title = "Mailing";

var endpointConfiguration = new EndpointConfiguration("Mailing");
endpointConfiguration.EnableInstallers();
    
var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
transport.ConnectionString("host=localhost;");
transport.UseConventionalRoutingTopology(QueueType.Classic);

endpointConfiguration.SendFailedMessagesTo("error");
endpointConfiguration.AuditProcessedMessagesTo("audit");
endpointConfiguration.SendHeartbeatTo("Particular.Learning");
    
var metrics = endpointConfiguration.EnableMetrics();
metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));

var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

Console.WriteLine("Press Enter to exit.");
Console.ReadLine();

await endpointInstance.Stop().ConfigureAwait(false);