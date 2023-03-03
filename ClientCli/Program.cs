using Messages;
using NServiceBus.Logging;

Console.Title = "Client CLI";

var endpointConfiguration = new EndpointConfiguration("ClientCli");
var transport = endpointConfiguration.UseTransport<LearningTransport>();
var routing = transport.Routing();

routing.RouteToEndpoint(typeof(BookVisit), "Booking");

var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

await RunLoop(endpointInstance, LogManager.GetLogger<Program>()).ConfigureAwait(false);
await endpointInstance.Stop().ConfigureAwait(false);

static async Task RunLoop(IEndpointInstance endpointInstance, ILog log)
{
    while (true)
    {
        log.Info("Press 'P' to place an order, or 'Q' to quit.");
        var key = Console.ReadKey();
        Console.WriteLine();

        switch (key.Key)
        {
            case ConsoleKey.P:
                var command = new BookVisit
                {
                    VisitTime = TimeOnly.FromDateTime(DateTime.Now).ToString()
                };

                log.Info($"Sending BookVisit command, VisitTime = {command.VisitTime}");
                await endpointInstance.Send(command).ConfigureAwait(false);

                break;

            case ConsoleKey.Q:
                return;

            default:
                log.Info("Unknown input. Please try again.");
                break;
        }
    }
}