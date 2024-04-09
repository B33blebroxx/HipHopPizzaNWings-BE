namespace HipHopPizzaNWings.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<OrderItem>? Order { get; set; }
    }
}
