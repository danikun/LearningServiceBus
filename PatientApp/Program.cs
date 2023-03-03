using Messages;
using PatientApp.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Host.UseNServiceBus(_ =>
{
    var endpointConfiguration = new EndpointConfiguration("Patient");
    endpointConfiguration.EnableInstallers();
    
    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString("host=localhost;");
    transport.UseConventionalRoutingTopology(QueueType.Classic);
    
    var routing = transport.Routing();
    routing.RouteToEndpoint(typeof(BookVisit), "Booking");

    endpointConfiguration.SendFailedMessagesTo("error");
    endpointConfiguration.AuditProcessedMessagesTo("audit");
    endpointConfiguration.SendHeartbeatTo("Particular.Learning");
    
    var metrics = endpointConfiguration.EnableMetrics();
    metrics.SendMetricDataToServiceControl("Particular.Monitoring", TimeSpan.FromMilliseconds(500));

    return endpointConfiguration;
});

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();
app.MapHub<BookingHub>("/bookingHub");

app.Run();