public class Ticket
{
    public int Id { get; }
    public int TripCode { get; }
    public int From { get; }
    public int To { get; }

    public bool IsValidated;

    public Ticket(int id, int tripCode, int from, int to)
    {
        Id = id;
        TripCode = tripCode;
        From = from;
        To = to;
        IsValidated = false;
    }
}