namespace Messages;

public class BookVisit : ICommand
{
    public string VisitId { get; set; }
    public string VisitTime { get; set; }
}