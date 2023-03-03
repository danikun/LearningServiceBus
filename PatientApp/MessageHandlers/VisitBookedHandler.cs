using Messages;
using Microsoft.AspNetCore.SignalR;
using NServiceBus.Logging;
using PatientApp.Hubs;

namespace PatientApp.MessageHandlers;

public class VisitBookedHandler : IHandleMessages<VisitBooked>
{
    private static readonly ILog Log = LogManager.GetLogger<VisitBookedHandler>();
    private readonly IHubContext<BookingHub, IBookingClient> _hubContext;

    public VisitBookedHandler(IHubContext<BookingHub, IBookingClient> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task Handle(VisitBooked message, IMessageHandlerContext context)
    {
        Log.Info($"handling booking with id: {message.VisitId}");
        return _hubContext.Clients.Group(message.VisitId).ConfirmBooking();
    }
}