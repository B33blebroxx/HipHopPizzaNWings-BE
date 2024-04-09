namespace HipHopPizzaNWings.Models
{
    public class CreateOrderDTO
    {
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int OrderTypeId { get; set; }
        public DateTime DateCreated { get; set; }
        public Boolean IsClosed { get; set; }
    }
}
