using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Mailing;

public class VisitBookedHandler : IHandleMessages<VisitBooked>
{
    private static readonly ILog Log = LogManager.GetLogger<VisitBookedHandler>();
    
    public Task Handle(VisitBooked message, IMessageHandlerContext context)
    {
        Log.Info($"Received VisitBooked, OrderId = {message.VisitId} - Sending confirmation email to patient...");

        return Task.CompletedTask;
    }
}