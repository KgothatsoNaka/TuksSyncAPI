namespace TuksSyncAPI.Models
{
    public class EventInfo
    {
        public required string Title { get; set; }
        public required string Location { get; set; }
        public decimal TicketPrice { get; set; }
        public int Id { get; set; }
    }
}