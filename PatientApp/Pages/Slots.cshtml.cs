using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PatientApp.Pages;

public class Slots : PageModel
{
    public Slots()
    {
        SlotCollection = Enumerable.Empty<Slot>();
    }

    public IEnumerable<Slot> SlotCollection { get; set; }

    public IActionResult OnGet()
    {
        SlotCollection = new[]
        {
            new Slot { Time = new TimeOnly(9, 0) },
            new Slot { Time = new TimeOnly(10, 0) },
            new Slot { Time = new TimeOnly(11, 0) },
            new Slot { Time = new TimeOnly(12, 0) },
            new Slot { Time = new TimeOnly(13, 0) },
            new Slot { Time = new TimeOnly(14, 0) },
            new Slot { Time = new TimeOnly(15, 0) },
            new Slot { Time = new TimeOnly(16, 0) },
            new Slot { Time = new TimeOnly(17, 0) },
            new Slot { Time = new TimeOnly(18, 0) },
        };

        return Page();
    }

    public async Task<IActionResult> OnPostSelectAsync(TimeOnly time)
    {
        Console.WriteLine(time.ToString());
        return RedirectToPage();
    }
}

public class Slot
{
    public TimeOnly Time { get; set; }
}