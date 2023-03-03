using Microsoft.AspNetCore.SignalR;

namespace PatientApp.Hubs;

public class BookingHub : Hub<IBookingClient>
{
    public Task Subscribe(string id)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, id);
    }
}

public interface IBookingClient
{
    Task ConfirmBooking();
}