using Messages;
using NServiceBus.Logging;

namespace BookingService;

public class BookVisitHandler : IHandleMessages<BookVisit>
{
    private static ILog _log = LogManager.GetLogger<BookVisitHandler>();
    
    public async Task Handle(BookVisit message, IMessageHandlerContext context)
    {
        _log.Info($"Book visit requested for {message.VisitId}");
        await Task.Delay(2000, context.CancellationToken);
        await context.Publish(new VisitBooked { VisitId = message.VisitId });
    }
}