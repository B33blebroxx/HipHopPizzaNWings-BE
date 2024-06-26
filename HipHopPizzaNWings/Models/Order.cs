﻿namespace HipHopPizzaNWings.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateClosed { get; set; }
        public int OrderTypeId { get; set; }
        public OrderType? OrderType { get; set; }
        public bool IsClosed { get; set; }
        public decimal? Subtotal
        {
            get
            {
                return Items?.Sum(i => i.Price);
            }
        }
        public decimal? Tip { get; set; }
        public decimal? Total
        {
            get
            {
                return (Tip + Subtotal);
            }
        }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
