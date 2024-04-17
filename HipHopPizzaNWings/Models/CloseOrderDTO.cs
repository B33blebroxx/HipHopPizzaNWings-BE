namespace HipHopPizzaNWings.Models
{
    public class CloseOrderDTO
    {
        public DateTime DateClosed { get; set; }
        public bool IsClosed { get; set; }
        public decimal? Tip { get; set; }
    }
}
