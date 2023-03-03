using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PatientApp.Pages;

public class BookVisit : PageModel
{
    private readonly IMessageSession  _messageSession;

    public BookVisit(IMessageSession messageSession)
    {
        _messageSession = messageSession;
    }

    public bool Confirmed { get; set; }
    public string? Id { get; set; }

    public async Task<IActionResult> OnGet(bool confirmed, TimeOnly time)
    {
        Confirmed = confirmed;
        
        if (!confirmed)
        {
            Id = Guid.NewGuid().ToString();
            await _messageSession.Send(new Messages.BookVisit { VisitId = Id, VisitTime = time.ToString() });
        }
        
        return Page();
    }
}